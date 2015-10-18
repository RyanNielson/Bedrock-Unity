using UnityEngine;
using UnityEditor;

namespace RyanNielson.Bedrock.AssetPostprocessors
{
    public class SpriteAssetPostprocessor : AssetPostprocessor
    {
        private void OnPreprocessTexture()
        {
            if (assetPath.ToLower().Contains("/sprites/"))
            {
                UpdateTextureImporter();
            }
        }

        private void UpdateTextureImporter()
        {
            TextureImporter importer = assetImporter as TextureImporter;

            importer.textureType = TextureImporterType.Sprite;
            importer.mipmapEnabled = false;
            importer.filterMode = FilterMode.Point;
            importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
        }
    }
}
