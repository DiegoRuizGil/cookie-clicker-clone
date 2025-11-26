using System.IO;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor
{
    public class CookieAssetsTool : EditorWindow
    {
        [MenuItem("Tools/Test")]
        public static void OpenWindow() => GetWindow<CookieAssetsTool>();

        [SerializeField] private string folderPath = "Assets/Cookie Clicker/Data";
        [SerializeField] private string buildingName;
        [SerializeField] private double baseCPS;
        [SerializeField] private double baseCost;
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite silhouette;
        
        private SerializedObject _so;
        private SerializedProperty _propFolderPath;
        private SerializedProperty _propBuildingName;
        private SerializedProperty _propBaseCPS;
        private SerializedProperty _propBaseCost;
        private SerializedProperty _propIcon;
        private SerializedProperty _propSilhouette;

        private void OnEnable()
        {
            _so = new SerializedObject(this);
            _propFolderPath = _so.FindProperty(nameof(folderPath));
            _propBuildingName = _so.FindProperty(nameof(buildingName));
            _propBaseCPS = _so.FindProperty(nameof(baseCPS));
            _propBaseCost = _so.FindProperty(nameof(baseCost));
            _propIcon = _so.FindProperty(nameof(icon));
            _propSilhouette = _so.FindProperty(nameof(silhouette));
        }

        private void OnGUI()
        {
            _so.Update();

            EditorGUILayout.PropertyField(_propFolderPath);
            EditorGUILayout.PropertyField(_propBuildingName);
            EditorGUILayout.PropertyField(_propBaseCPS);
            EditorGUILayout.PropertyField(_propBaseCost);
            EditorGUILayout.PropertyField(_propIcon);
            EditorGUILayout.PropertyField(_propSilhouette);

            if (GUILayout.Button("Create Asset"))
            {
                var id = CreateID();
                var idPath = Path.Combine(_propFolderPath.stringValue, _propBuildingName.stringValue + "ID.asset");
                AssetDatabase.CreateAsset(id, idPath);
                
                var asset = CreateBuilding(id);
                var path = Path.Combine(_propFolderPath.stringValue, _propBuildingName.stringValue + ".asset");
                AssetDatabase.CreateAsset(asset, path);
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            _so.ApplyModifiedProperties();
        }

        private BuildingID CreateID()
        {
            var id = CreateInstance<BuildingID>();
            var so = new SerializedObject(id);

            var propName = so.FindProperty("buildingName");
            propName.stringValue = _propBuildingName.stringValue;

            so.ApplyModifiedProperties();
            
            return id;
        }

        private BuildingConfig CreateBuilding(BuildingID id)
        {
            var asset = CreateInstance<BuildingConfig>();
            var so = new SerializedObject(asset);

            so.FindProperty("buildingID").objectReferenceValue = id;
            so.FindProperty("baseCPS").doubleValue = _propBaseCPS.doubleValue;
            so.FindProperty("baseCost").doubleValue = _propBaseCost.doubleValue;
            so.FindProperty("icon").objectReferenceValue = _propIcon.objectReferenceValue;
            so.FindProperty("silhouette").objectReferenceValue = _propSilhouette.objectReferenceValue;
            
            so.ApplyModifiedProperties();
            
            return asset;
        }
    }
}