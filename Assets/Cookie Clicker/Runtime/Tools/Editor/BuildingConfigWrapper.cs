using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor
{
    public class BuildingConfigWrapper
    {
        public BuildingConfig Config { get; private set; }
        public SerializedObject SO { get; private set; }
        public SerializedProperty PropID { get; set; }
        public SerializedProperty PropBaseCps { get; set; }
        public SerializedProperty PropBaseCost { get; set; }
        public SerializedProperty PropIcon { get; set; }
        public SerializedProperty PropSilhouette { get; set; }
        

        public BuildingConfigWrapper()
        {
            Reset();
        }

        public void Reset()
        {
            Config = ScriptableObject.CreateInstance<BuildingConfig>();

            SO = new SerializedObject(Config);
            PropID = SO.FindProperty("buildingID");
            PropBaseCps = SO.FindProperty("baseCPS");
            PropBaseCost = SO.FindProperty("baseCost");
            PropIcon = SO.FindProperty("icon");
            PropSilhouette = SO.FindProperty("silhouette");
        }
    }
}