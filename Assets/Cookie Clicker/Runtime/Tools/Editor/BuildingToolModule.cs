using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
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

        private List<BuildingConfig> _currentBuildings;
        
        public BuildingToolModule(EditorWindow window, string folderPath)
        {
            _window = window;
            _folderPath = folderPath;
            
            _currentConfig = new  BuildingConfigWrapper();
            _currentID = new  BuildingIDWrapper();

            ResetCurrentObjects();

            _currentBuildings = FindBuildings();
            
            SelectFromList(_selectedIndex);
        }
        
        public void DrawList()
        {
            using var scroll = new EditorGUILayout.ScrollViewScope(_buildingListScrollPos, EditorStyles.helpBox, GUILayout.MaxWidth(150));
            _buildingListScrollPos = scroll.scrollPosition;
                
            for (int i = 0; i < _currentBuildings.Count; i++)
            {
                Rect rowRect = GUILayoutUtility.GetRect(0, 20, GUILayout.ExpandWidth(true));
                
                bool isSelected = i == _selectedIndex;
                Color bgColor = isSelected
                    ? ToolUtils.SelectedColor
                    : (i % 2 == 0) ? ToolUtils.DarkColor1 : ToolUtils.DarkColor2;
                EditorGUI.DrawRect(rowRect, bgColor);
                    
                GUI.Label(new Rect(rowRect.x + 5, rowRect.y, rowRect.width, rowRect.height), _currentBuildings[i].buildingID);

                if (Event.current.type == EventType.MouseDown && rowRect.Contains(Event.current.mousePosition))
                {
                    SelectFromList(i);
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

                _currentBuildings = FindBuildings();
            }
            
            EditorGUILayout.EndVertical();
        }

        private void ResetCurrentObjects()
        {
            _currentConfig.Reset();
            _currentID.Reset();
        }

        private void SelectFromList(int index)
        {
            _selectedIndex = index;
            
            _currentConfig.Set(_currentBuildings[index]);
            _currentID.Set(_currentBuildings[index].buildingID);
        }

        private List<BuildingConfig> FindBuildings()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BuildingConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var buildings = paths.Select(AssetDatabase.LoadAssetAtPath<BuildingConfig>)
                .Where(config => config.buildingID).ToList();
            
            return buildings;
        }
    }
}