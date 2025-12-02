using System.IO;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor
{
    public static class ToolUtils
    {
        public static readonly Color DarkColor1 = new Color(0.25f, 0.25f, 0.25f);
        public static readonly Color DarkColor2 = new Color(0.3f, 0.3f, 0.3f);
        public static readonly Color SelectedColor = new Color(0.192f, 0.301f, 0.474f);

        private static readonly string DefaultAssetsPath = "Assets/Cookie Clicker/Editor/DefaultAssets";

        public static Sprite LoadIcon(string icon)
        {
            var path = Path.Combine(DefaultAssetsPath, "Icons", $"{icon}.png");
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }
    }
}