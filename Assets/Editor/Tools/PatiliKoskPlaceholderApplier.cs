#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using Vampire;

namespace VampireEditor
{
    public class PatiliKoskPlaceholderApplier
    {
        [MenuItem("PatiliKosk/Tools/Apply Kenney Placeholders")]
        public static void ApplyPlaceholders()
        {
            Debug.Log("[PatiliKosk] Starting visual placeholder migration...");

            // 1. Texture Paths
            string playerPath = "Assets/_PatiliKosk/Art/Placeholders/Kenney/cat_guardian_placeholder.png";
            string enemyPath = "Assets/_PatiliKosk/Art/Placeholders/Kenney/shadow_enemy_placeholder.png";
            string coinPath = "Assets/_PatiliKosk/Art/Placeholders/Kenney/food_coin_placeholder.png";
            string gemPath = "Assets/_PatiliKosk/Art/Placeholders/Kenney/exp_gem_placeholder.png";

            string[] texturePaths = new string[] { playerPath, enemyPath, coinPath, gemPath };

            // 2. Configure Texture Importers
            foreach (var path in texturePaths)
            {
                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                if (importer == null)
                {
                    Debug.LogError($"[PatiliKosk] Texture asset not found at path: {path}");
                    continue;
                }

                // Sprite (2D and UI) settings
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Single;
                importer.alphaIsTransparency = true;
                importer.mipmapEnabled = false; // Disable MipMaps as requested
                importer.filterMode = FilterMode.Point; // Pixel art point filter

                // Android Settings
                TextureImporterPlatformSettings androidSettings = importer.GetPlatformTextureSettings("Android");
                androidSettings.overridden = true;
                androidSettings.maxTextureSize = 2048;
                androidSettings.textureCompression = TextureImporterCompression.Compressed;
                androidSettings.compressionQuality = 50;
                importer.SetPlatformTextureSettings(androidSettings);

                // Default Settings
                TextureImporterPlatformSettings defaultSettings = importer.GetDefaultPlatformTextureSettings();
                defaultSettings.textureCompression = TextureImporterCompression.Compressed;
                importer.SetPlatformTextureSettings(defaultSettings);

                importer.SaveAndReimport();
                Debug.Log($"[PatiliKosk] Configured and reimported: {path}");
            }

            // 3. Load Sprites
            Sprite playerSprite = AssetDatabase.LoadAssetAtPath<Sprite>(playerPath);
            Sprite enemySprite = AssetDatabase.LoadAssetAtPath<Sprite>(enemyPath);
            Sprite coinSprite = AssetDatabase.LoadAssetAtPath<Sprite>(coinPath);
            Sprite gemSprite = AssetDatabase.LoadAssetAtPath<Sprite>(gemPath);

            if (playerSprite == null || enemySprite == null || coinSprite == null || gemSprite == null)
            {
                Debug.LogError("[PatiliKosk] One or more placeholder sprites could not be loaded after reimport!");
                return;
            }

            // 4. Update Character Blueprints
            string[] characterBlueprintPaths = new string[]
            {
                "Assets/Blueprints/Characters/Main Character Blueprint.asset",
                "Assets/Blueprints/Characters/Test Character Blueprint 1.asset",
                "Assets/Blueprints/Characters/Test Character Blueprint 2.asset"
            };

            foreach (var path in characterBlueprintPaths)
            {
                var bp = AssetDatabase.LoadAssetAtPath<CharacterBlueprint>(path);
                if (bp != null)
                {
                    bp.walkSpriteSequence = new Sprite[] { playerSprite };
                    EditorUtility.SetDirty(bp);
                    Debug.Log($"[PatiliKosk] Updated Character Blueprint: {path}");
                }
                else
                {
                    Debug.LogWarning($"[PatiliKosk] Character Blueprint not found at: {path}");
                }
            }

            // 5. Update Enemy Blueprints
            string[] monsterGuids = AssetDatabase.FindAssets("t:MonsterBlueprint", new string[] { "Assets/Blueprints/Monsters" });
            foreach (var guid in monsterGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var bp = AssetDatabase.LoadAssetAtPath<MonsterBlueprint>(path);
                if (bp != null)
                {
                    bp.walkSpriteSequence = new Sprite[] { enemySprite };
                    EditorUtility.SetDirty(bp);
                    Debug.Log($"[PatiliKosk] Updated Monster Blueprint: {path}");
                }
            }

            // 6. Update Coin Blueprint
            var coinBp = AssetDatabase.LoadAssetAtPath<CoinBlueprint>("Assets/Blueprints/Coin/Coin.asset");
            if (coinBp != null)
            {
                SerializedObject serializedCoinBp = new SerializedObject(coinBp);
                SerializedProperty coinSpritesProperty = serializedCoinBp.FindProperty("coinSprites");
                if (coinSpritesProperty != null)
                {
                    SerializedProperty contentProperty = coinSpritesProperty.FindPropertyRelative("content");
                    if (contentProperty != null && contentProperty.isArray)
                    {
                        for (int i = 0; i < contentProperty.arraySize; i++)
                        {
                            contentProperty.GetArrayElementAtIndex(i).objectReferenceValue = coinSprite;
                        }
                        serializedCoinBp.ApplyModifiedProperties();
                        EditorUtility.SetDirty(coinBp);
                        Debug.Log("[PatiliKosk] Updated Coin Blueprint coinSprites content with food_coin_placeholder.");
                    }
                    else
                    {
                        Debug.LogError("[PatiliKosk] coinSprites.content property not found or is not an array!");
                    }
                }
                else
                {
                    Debug.LogError("[PatiliKosk] coinSprites property not found!");
                }
            }
            else
            {
                Debug.LogError("[PatiliKosk] Coin Blueprint not found at Assets/Blueprints/Coin/Coin.asset");
            }

            // 7. Update Exp Gem Blueprint
            var expGemBp = AssetDatabase.LoadAssetAtPath<ExpGemBlueprint>("Assets/Blueprints/Exp Gem/Exp Gem.asset");
            if (expGemBp != null)
            {
                SerializedObject serializedExpGemBp = new SerializedObject(expGemBp);
                SerializedProperty gemSpritesProperty = serializedExpGemBp.FindProperty("gemSpritesAndColors");
                if (gemSpritesProperty != null)
                {
                    SerializedProperty content1Property = gemSpritesProperty.FindPropertyRelative("content1");
                    if (content1Property != null && content1Property.isArray)
                    {
                        for (int i = 0; i < content1Property.arraySize; i++)
                        {
                            content1Property.GetArrayElementAtIndex(i).objectReferenceValue = gemSprite;
                        }
                        serializedExpGemBp.ApplyModifiedProperties();
                        EditorUtility.SetDirty(expGemBp);
                        Debug.Log("[PatiliKosk] Updated Exp Gem Blueprint gemSpritesAndColors content1 with exp_gem_placeholder.");
                    }
                    else
                    {
                        Debug.LogError("[PatiliKosk] gemSpritesAndColors.content1 property not found or is not an array!");
                    }
                }
                else
                {
                    Debug.LogError("[PatiliKosk] gemSpritesAndColors property not found!");
                }
            }
            else
            {
                Debug.LogError("[PatiliKosk] Exp Gem Blueprint not found at Assets/Blueprints/Exp Gem/Exp Gem.asset");
            }

            AssetDatabase.SaveAssets();
            Debug.Log("[PatiliKosk] Visual placeholders apply completed successfully!");
        }
    }
}
#endif
