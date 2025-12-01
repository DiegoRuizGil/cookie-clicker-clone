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

        private Vector2 _buildingListScrollPos;
        private int _selectedIndex;

        private readonly Color _dark1 = new Color(0.25f, 0.25f, 0.25f);
        private readonly Color _dark2 = new Color(0.3f, 0.3f, 0.3f);
        private readonly Color _selectionColor = new Color(0.192f, 0.301f, 0.474f);

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
            GUILayout.BeginArea(new Rect(10, 10, position.width - 20, position.height - 20));
            
            using (new EditorGUILayout.HorizontalScope())
            {
                DrawBuildingList();
                DrawBuildingCreation();
            }
            
            GUILayout.EndArea();
        }

        private void DrawBuildingList()
        {
            using var scroll = new EditorGUILayout.ScrollViewScope(_buildingListScrollPos, EditorStyles.helpBox, GUILayout.MaxWidth(150));
            _buildingListScrollPos = scroll.scrollPosition;
                
            for (int i = 0; i < 100; i++)
            {
                Rect rowRect = GUILayoutUtility.GetRect(0, 20, GUILayout.ExpandWidth(true));
                
                bool isSelected = i == _selectedIndex;
                Color bgColor = isSelected
                    ? _selectionColor
                    : (i % 2 == 0) ? _dark1 : _dark2;
                EditorGUI.DrawRect(rowRect, bgColor);
                    
                GUI.Label(new Rect(rowRect.x + 5, rowRect.y, rowRect.width, rowRect.height), $"Building - {i}");

                if (Event.current.type == EventType.MouseDown && rowRect.Contains(Event.current.mousePosition))
                {
                    _selectedIndex = i;
                    Repaint();
                }
            }
        }

        private void DrawBuildingCreation()
        {
            EditorGUILayout.BeginVertical();
            
            _so.Update();
            EditorGUILayout.PropertyField(_propFolderPath);
            EditorGUILayout.PropertyField(_propBuildingName);
            EditorGUILayout.PropertyField(_propBaseCPS);
            EditorGUILayout.PropertyField(_propBaseCost);
            _propIcon.objectReferenceValue = EditorGUILayout.ObjectField("Icon", _propIcon.objectReferenceValue, typeof(Sprite), false);
            _propSilhouette.objectReferenceValue = EditorGUILayout.ObjectField("Silhouette", _propSilhouette.objectReferenceValue, typeof(Sprite), false);
            _so.ApplyModifiedProperties();

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
            
            EditorGUILayout.EndVertical();
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