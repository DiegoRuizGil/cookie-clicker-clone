using System.IO;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor
{
    public class BuildingToolModule
    {
        private readonly BuildingConfigWrapper _currentConfig;
        private readonly BuildingIDWrapper _currentID;
        
        private Vector2 _buildingListScrollPos;
        private int _selectedIndex;
        
        private readonly EditorWindow _window;
        private readonly string _folderPath;
        
        public BuildingToolModule(EditorWindow window, string folderPath)
        {
            _window = window;
            _folderPath = folderPath;
            
            _currentConfig = new  BuildingConfigWrapper();
            _currentID = new  BuildingIDWrapper();

            ResetCurrentObjects();
        }
        
        public void DrawList()
        {
            using var scroll = new EditorGUILayout.ScrollViewScope(_buildingListScrollPos, EditorStyles.helpBox, GUILayout.MaxWidth(150));
            _buildingListScrollPos = scroll.scrollPosition;
                
            for (int i = 0; i < 100; i++)
            {
                Rect rowRect = GUILayoutUtility.GetRect(0, 20, GUILayout.ExpandWidth(true));
                
                bool isSelected = i == _selectedIndex;
                Color bgColor = isSelected
                    ? ToolUtils.SelectedColor
                    : (i % 2 == 0) ? ToolUtils.DarkColor1 : ToolUtils.DarkColor2;
                EditorGUI.DrawRect(rowRect, bgColor);
                    
                GUI.Label(new Rect(rowRect.x + 5, rowRect.y, rowRect.width, rowRect.height), $"Building - {i}");

                if (Event.current.type == EventType.MouseDown && rowRect.Contains(Event.current.mousePosition))
                {
                    _selectedIndex = i;
                    _window.Repaint();
                }
            }
        }

        public void DrawEditor()
        {
            EditorGUILayout.BeginVertical();
            
            _currentID.SO.Update();
            EditorGUILayout.PropertyField(_currentID.PropName);
            _currentID.SO.ApplyModifiedProperties();
            
            _currentConfig.SO.Update();
            EditorGUILayout.PropertyField(_currentConfig.PropBaseCps);
            EditorGUILayout.PropertyField(_currentConfig.PropBaseCost);
            _currentConfig.PropIcon.objectReferenceValue = EditorGUILayout.ObjectField("Icon", _currentConfig.PropIcon.objectReferenceValue, typeof(Sprite), false);
            _currentConfig.PropSilhouette.objectReferenceValue = EditorGUILayout.ObjectField("Silhouette", _currentConfig.PropSilhouette.objectReferenceValue, typeof(Sprite), false);
            _currentConfig.PropID.objectReferenceValue = _currentID.ID;
            _currentConfig.SO.ApplyModifiedProperties();

            if (GUILayout.Button("Create/Update"))
            {
                string name = _currentID.ID;
                var idPath = Path.Combine(_folderPath, name + "ID.asset");
                var configPath = Path.Combine(_folderPath, name + ".asset");
                
                AssetDatabase.CreateAsset(_currentID.ID, idPath);
                AssetDatabase.CreateAsset(_currentConfig.Config, configPath);
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            EditorGUILayout.EndVertical();
        }

        private void ResetCurrentObjects()
        {
            _currentConfig.Reset();
            _currentID.Reset();
        }
    }
}