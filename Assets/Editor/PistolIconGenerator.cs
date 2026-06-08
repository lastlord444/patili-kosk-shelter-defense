using UnityEngine;
using UnityEditor;
using System.IO;

namespace Vampire
{
    public static class PistolIconGenerator
    {
        [MenuItem("Tools/Generate Pistol Icon")]
        public static void GeneratePistolIcon()
        {
            int width = 32;
            int height = 32;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            
            // Fill with transparent color
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tex.SetPixel(x, y, Color.clear);
                }
            }
            
            // Draw a simple retro 32x32 pixel art pistol
            Color gripColor = new Color(0.45f, 0.25f, 0.1f, 1f); // Brown
            Color gripShadow = new Color(0.35f, 0.2f, 0.08f, 1f); // Dark Brown
            
            for (int y = 6; y <= 13; y++)
            {
                // Slanted grip
                int xOffset = (y - 6) / 2;
                for (int x = 10 - xOffset; x <= 13 - xOffset; x++)
                {
                    if (x == 10 - xOffset)
                    {
                        tex.SetPixel(x, y, gripShadow);
                    }
                    else
                    {
                        tex.SetPixel(x, y, gripColor);
                    }
                }
            }
            
            // Barrel and Slide (Dark metal gray)
            Color metalColor = new Color(0.3f, 0.3f, 0.35f, 1f); // Grey-Blue metal
            Color metalHighlight = new Color(0.5f, 0.5f, 0.55f, 1f); // Light metal highlight
            Color metalShadow = new Color(0.15f, 0.15f, 0.18f, 1f); // Dark metal shadow
            
            for (int y = 14; y <= 18; y++)
            {
                for (int x = 6; x <= 24; x++)
                {
                    if (y == 18)
                    {
                        tex.SetPixel(x, y, metalHighlight);
                    }
                    else if (y == 14)
                    {
                        tex.SetPixel(x, y, metalShadow);
                    }
                    else
                    {
                        tex.SetPixel(x, y, metalColor);
                    }
                }
            }
            
            // Trigger guard (Dark metal)
            Color triggerColor = new Color(0.2f, 0.2f, 0.22f, 1f);
            tex.SetPixel(13, 11, triggerColor);
            tex.SetPixel(13, 12, triggerColor);
            tex.SetPixel(14, 11, triggerColor);
            tex.SetPixel(12, 12, triggerColor);
            
            // Trigger itself (Light metal)
            tex.SetPixel(11, 12, metalHighlight);
            
            tex.Apply();
            
            // Save to PNG
            string dirPath = Application.dataPath + "/Sprites/Weapons";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            
            string filePath = dirPath + "/Pistol.png";
            byte[] bytes = tex.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
            
            AssetDatabase.Refresh();
            
            // Configure texture import settings
            string relativePath = "Assets/Sprites/Weapons/Pistol.png";
            TextureImporter importer = AssetImporter.GetAtPath(relativePath) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spritePixelsPerUnit = 32;
                importer.filterMode = FilterMode.Point;
                importer.mipmapEnabled = false;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                importer.SaveAndReimport();
            }
            
            // Assign to Pistol Ability prefab
            string prefabPath = "Assets/Prefabs/Abilities/Projectile Abilities/Pistol Ability.prefab";
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab == null)
            {
                Debug.LogWarning("[PistolIconGenerator] Prefab not found at path: " + prefabPath);
            }
            else
            {
                var ability = prefab.GetComponent<Ability>();
                if (ability == null)
                {
                    Debug.LogWarning("[PistolIconGenerator] Ability component not found on prefab.");
                }
                else
                {
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(relativePath);
                    if (sprite == null)
                    {
                        Debug.LogWarning("[PistolIconGenerator] Sprite asset not found at path: " + relativePath);
                    }
                    else
                    {
                        SerializedObject so = new SerializedObject(ability);
                        SerializedProperty prop = so.FindProperty("image");
                        if (prop == null)
                        {
                            Debug.LogWarning("[PistolIconGenerator] 'image' property not found on Ability component.");
                        }
                        else
                        {
                            prop.objectReferenceValue = sprite;
                            so.ApplyModifiedProperties();
                            
                            // Also update the SpriteRenderer's sprite on the prefab if it has one
                            SpriteRenderer sr = prefab.GetComponentInChildren<SpriteRenderer>();
                            if (sr != null)
                            {
                                SerializedObject srSo = new SerializedObject(sr);
                                SerializedProperty srSpriteProp = srSo.FindProperty("m_Sprite");
                                if (srSpriteProp != null)
                                {
                                    srSpriteProp.objectReferenceValue = sprite;
                                    srSo.ApplyModifiedProperties();
                                }
                            }
                            
                            PrefabUtility.SavePrefabAsset(prefab);
                            Debug.Log("[PistolIconGenerator] Successfully assigned Pistol sprite to Pistol Ability prefab.");
                        }
                    }
                }
            }
            
            Debug.Log("[PistolIconGenerator] Procedural 32x32 Pistol icon generated and imported successfully.");
        }
    }
}
