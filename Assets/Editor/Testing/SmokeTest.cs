#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using Vampire;

namespace VampireEditor
{
    public class SmokeTest
    {
        [MenuItem("SmokeTest/Run")]
        public static void Run()
        {
            Debug.Log("[SmokeTest] Starting Smoke Test Setup...");
            
            // Open Level 1 scene
            EditorSceneManager.OpenScene("Assets/Scenes/Game/Level 1.unity");
            
            // Create Initializer in Editor Mode
            GameObject initGo = new GameObject("SmokeTestInitializer");
            var initializer = initGo.AddComponent<SmokeTestInitializer>();
            
            // Load character blueprint and assign
            var blueprint = AssetDatabase.LoadAssetAtPath<CharacterBlueprint>("Assets/Blueprints/Characters/Main Character Blueprint.asset");
            if (blueprint == null)
            {
                Debug.LogError("[SmokeTest] Failed to load character blueprint in Editor Mode!");
                return;
            }
            initializer.defaultBlueprint = blueprint;
            
            // Set playing to true to enter Play Mode
            EditorApplication.isPlaying = true;
        }

        [MenuItem("SmokeTest/BuildAndroid")]
        public static void BuildAndroid()
        {
            Debug.Log("[SmokeTest] Starting Android Build Smoke Test...");
            
            // Switch to Android platform
            bool switchResult = EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            Debug.Log($"[SmokeTest] Switch to Android result: {switchResult}");

            // Gather scenes from EditorBuildSettings
            var scenes = EditorBuildSettings.scenes;
            if (scenes == null || scenes.Length == 0)
            {
                Debug.LogWarning("[SmokeTest] No scenes in build settings. Adding active scene...");
                var activeScene = EditorSceneManager.GetActiveScene();
                if (string.IsNullOrEmpty(activeScene.path))
                {
                    EditorSceneManager.OpenScene("Assets/Scenes/Game/Level 1.unity");
                    activeScene = EditorSceneManager.GetActiveScene();
                }
                scenes = new EditorBuildSettingsScene[] { new EditorBuildSettingsScene(activeScene.path, true) };
            }

            string[] scenePaths = new string[scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
            {
                scenePaths[i] = scenes[i].path;
                Debug.Log($"[SmokeTest] Scene in build: {scenePaths[i]}");
            }

            // Define build settings
            string buildPath = "Build/android_smoke.apk";
            Directory.CreateDirectory("Build");

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = scenePaths;
            buildPlayerOptions.locationPathName = buildPath;
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = BuildOptions.None;

            // Trigger build
            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            var summary = report.summary;

            string reportPath = "android_build_report.txt";
            string reportContent = $"Android Build Report:\n" +
                                   $"- Result: {summary.result}\n" +
                                   $"- Total Errors: {summary.totalErrors}\n" +
                                   $"- Total Warnings: {summary.totalWarnings}\n" +
                                   $"- Output Size: {summary.totalSize} bytes\n" +
                                   $"- Time: {summary.totalTime.TotalSeconds} seconds\n";

            if (summary.result == UnityEditor.Build.Reporting.BuildResult.Failed)
            {
                reportContent += "\nErrors:\n";
                foreach (var step in report.steps)
                {
                    foreach (var msg in step.messages)
                    {
                        if (msg.type == LogType.Error || msg.type == LogType.Exception)
                        {
                            reportContent += $"[{msg.type}] {msg.content}\n";
                        }
                    }
                }
            }

            File.WriteAllText(reportPath, reportContent);
            Debug.Log($"[SmokeTest] Android build finished! Result: {summary.result}. Report written to {reportPath}");
            
            EditorApplication.Exit(summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded ? 0 : 1);
        }
    }

    public class SmokeTestInitializer : MonoBehaviour
    {
        public CharacterBlueprint defaultBlueprint;

        void Awake()
        {
            Debug.Log("[SmokeTestInitializer] Awake called.");
            if (defaultBlueprint != null)
            {
                CrossSceneData.CharacterBlueprint = defaultBlueprint;
                Debug.Log($"[SmokeTestInitializer] Successfully assigned CrossSceneData.CharacterBlueprint to {defaultBlueprint.name}");
            }
            else
            {
                Debug.LogError("[SmokeTestInitializer] defaultBlueprint is null in Awake!");
            }
            
            // Create the observer that will monitor the play mode
            GameObject observerGo = new GameObject("SmokeTestObserver");
            observerGo.AddComponent<SmokeTestObserver>();
        }
    }

    public class SmokeTestObserver : MonoBehaviour
    {
        private float timer = 0f;
        private bool characterFound = false;
        private bool enemySpawned = false;
        private Vector3 initialPosition;
        private Character character;
        private string logOutput = "";

        void Start()
        {
            Debug.Log("[SmokeTestObserver] Start monitoring gameplay...");
            character = Object.FindFirstObjectByType<Character>();
            if (character != null)
            {
                characterFound = true;
                initialPosition = character.transform.position;
                Debug.Log($"[SmokeTestObserver] Found player character at {initialPosition}");
                
                // Find TouchJoystick in scene
                var joystick = Object.FindFirstObjectByType<TouchJoystick>();
                if (joystick != null)
                {
                    Debug.Log("[SmokeTestObserver] Found TouchJoystick. Simulating joystick drag right...");
                    joystick.StartTouch(Vector2.zero);
                    joystick.UpdateTouch(Vector2.right * 100f); // 100f is the joystickRadius
                }
                else
                {
                    Debug.LogError("[SmokeTestObserver] TouchJoystick NOT found in scene!");
                    // Fallback to direct movement if joystick is missing
                    character.Move(Vector2.right);
                    character.StartWalkAnimation();
                }
            }
            else
            {
                Debug.LogWarning("[SmokeTestObserver] Player character NOT found!");
            }

            Application.logMessageReceived += HandleLog;
        }

        void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                logOutput += $"[{type}] {logString}\n{stackTrace}\n";
            }
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (character != null && timer > 2.0f && timer < 2.2f)
            {
                Vector3 currentPos = character.transform.position;
                if (currentPos != initialPosition)
                {
                    Debug.Log($"[SmokeTestObserver] Player movement verified! Moved from {initialPosition} to {currentPos}");
                }
                else
                {
                    if (character.Velocity != Vector2.zero)
                    {
                        Debug.Log($"[SmokeTestObserver] Player movement verified via velocity: {character.Velocity}");
                    }
                    else
                    {
                        Debug.LogWarning($"[SmokeTestObserver] Player movement position and velocity remains unchanged! Position: {currentPos}");
                    }
                }
            }

            if (!enemySpawned)
            {
                Monster[] monsters = Object.FindObjectsByType<Monster>(FindObjectsSortMode.None);
                if (monsters.Length > 0)
                {
                    enemySpawned = true;
                    Debug.Log($"[SmokeTestObserver] Enemy spawn verified! Found {monsters.Length} monsters active.");
                }
            }

            if (timer >= 15.0f)
            {
                string reportPath = Path.Combine(Application.dataPath, "../smoke_test_report.txt");
                bool playerMoved = false;
                if (character != null)
                {
                    playerMoved = character.transform.position != initialPosition || character.Velocity != Vector2.zero;
                }
                
                string result = $"Smoke Test Report:\n" +
                               $"- Player Found: {characterFound}\n" +
                               $"- Player Moved: {playerMoved}\n" +
                               $"- Enemies Spawned: {enemySpawned}\n" +
                               $"- Runtime Errors/Exceptions:\n{(string.IsNullOrEmpty(logOutput) ? "None" : logOutput)}\n" +
                               $"- Time Elapsed: {timer}s\n";
                
                File.WriteAllText(reportPath, result);
                Debug.Log($"[SmokeTestObserver] Smoke test finished! Report written to {reportPath}");
                
                // Clean up the initializer from the scene
                var initGo = GameObject.Find("SmokeTestInitializer");
                if (initGo != null)
                {
                    DestroyImmediate(initGo);
                }
                
                EditorApplication.isPlaying = false;
                EditorApplication.Exit(0);
            }
        }
    }
}
#endif
