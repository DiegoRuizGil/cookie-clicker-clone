using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cookie_Clicker.Runtime.Tools.Editor.Buildings_Module
{
    public class BuildingToolModule
    {
        private readonly BuildingConfigWrapper _currentConfig;
        private readonly BuildingIDWrapper _currentID;

        private string _bufferName;
        private float _bufferBaseCps;
        private float _bufferBaseCost;
        private Sprite _bufferIcon;
        private Sprite _bufferSilhouette;
        private bool _hasPendingChanges;
        
        private Vector2 _buildingListScrollPos;
        private int _selectedIndex;

        private string _searchText;
        
        private List<BuildingConfig> _currentBuildings;
        
        private readonly EditorWindow _window;
        private readonly string _folderPath;

        private static readonly GUIContent TrashIcon = EditorGUIUtility.IconContent("TreeEditor.Trash");
        private static readonly GUIContent PlusIcon = EditorGUIUtility.IconContent("Toolbar Plus");
        
        public BuildingToolModule(EditorWindow window, string folderPath)
        {
            _window = window;
            _folderPath = folderPath;
            
            _currentConfig = new  BuildingConfigWrapper();
            _currentID = new  BuildingIDWrapper();

            ResetCurrentObjects();

            _currentBuildings = FindAllBuildings();
            
            SelectFromList(_selectedIndex);
        }

        public void OnGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                DrawList();
                DrawEditor();
            }
        }
        
        private void DrawList()
        {
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(200));

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            
            if (GUILayout.Button(PlusIcon, EditorStyles.toolbarButton, GUILayout.Width(25)))
                CreateNewBuilding();

            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                _searchText = GUILayout.TextField(_searchText, GUI.skin.FindStyle("ToolbarSearchTextField"));
                if (changeCheck.changed)
                    _currentBuildings = FindBuildingsByText(_searchText);
            }
            
            EditorGUILayout.EndHorizontal();

            using (var scroll = new EditorGUILayout.ScrollViewScope(_buildingListScrollPos, EditorStyles.helpBox))
            {
                _buildingListScrollPos = scroll.scrollPosition;
                
                for (int i = 0; i < _currentBuildings.Count; i++)
                {
                    var rowRect = GUILayoutUtility.GetRect(0, 20, GUILayout.ExpandWidth(true));
                
                    var isSelected = i == _selectedIndex;
                    var bgColor = isSelected
                        ? ToolUtils.SelectedColor
                        : (i % 2 == 0) ? ToolUtils.DarkColor1 : ToolUtils.DarkColor2;
                    EditorGUI.DrawRect(rowRect, bgColor);
                    
                    GUI.Label(new Rect(rowRect.x + 5, rowRect.y, rowRect.width - 30, rowRect.height), _currentBuildings[i].buildingID);

                    var iconRect = new Rect(rowRect.xMax - 20, rowRect.y + 2, 16, 16);

                    if (GUI.Button(iconRect, TrashIcon, GUIStyle.none))
                    {
                        DeleteBuilding(_currentBuildings[i]);
                        break;
                    }

                    if (Event.current.type == EventType.MouseDown && rowRect.Contains(Event.current.mousePosition))
                    {
                        SelectFromList(i);
                        _window.Repaint();
                    }
                }
            }

            if (GUILayout.Button("Load in Scene"))
                LoadBuildingsInScene();
            
            EditorGUILayout.EndVertical();
        }

        private void DrawEditor()
        {
            EditorGUILayout.BeginVertical();
            
            EditorGUI.BeginChangeCheck();
            
            GUI.SetNextControlName("Name");
            _bufferName = EditorGUILayout.TextField("Name", _bufferName);
            _bufferBaseCps = EditorGUILayout.FloatField("Base CPS", _bufferBaseCps);
            _bufferBaseCost = EditorGUILayout.FloatField("Base Cost", _bufferBaseCost);
            _bufferIcon = (Sprite)EditorGUILayout.ObjectField("Icon", _bufferIcon, typeof(Sprite), false);
            _bufferSilhouette = (Sprite)EditorGUILayout.ObjectField("Silhouette", _bufferSilhouette, typeof(Sprite), false);
            
            if (EditorGUI.EndChangeCheck())
                _hasPendingChanges = true;
            
            EditorGUILayout.Space(10);

            using (new EditorGUI.DisabledScope(!_hasPendingChanges))
            {
                if (GUILayout.Button("Apply"))
                    ApplyChanges();

                if (GUILayout.Button("Revert"))
                    RevertChanges();
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
            if (index < 0)
                index = _currentBuildings.Count - 1;
            _selectedIndex = index;
            
            _currentConfig.Set(_currentBuildings[index]);
            _currentID.Set(_currentBuildings[index].buildingID);

            SetBuffersValues();
        }

        private void DeselectFromList()
        {
            _selectedIndex = -1;
            
            ResetCurrentObjects();
        }

        private List<BuildingConfig> FindAllBuildings()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BuildingConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var buildings = paths.Select(AssetDatabase.LoadAssetAtPath<BuildingConfig>)
                .Where(config => config.buildingID).OrderBy(config => config.BaseCost).ToList();
            
            return buildings;
        }

        private List<BuildingConfig> FindBuildingsByText(string text)
        {
            return FindAllBuildings().Where(config =>
            {
                var name = ((string)config.buildingID).ToLower();
                return name.Contains(text.ToLower());
            }).ToList();
        }

        private void CreateNewBuilding()
        {
            DeselectFromList();
            
            _currentID.SO.Update();
            _currentID.PropName.stringValue = "New Building";
            _currentID.SO.ApplyModifiedProperties();
            _currentConfig.SO.Update();
            _currentConfig.PropIcon.objectReferenceValue = ToolUtils.LoadIcon("default-icon");
            _currentConfig.PropSilhouette.objectReferenceValue = ToolUtils.LoadIcon("default-icon");
            _currentConfig.PropID.objectReferenceValue = _currentID.ID;
            _currentConfig.SO.ApplyModifiedProperties();
            
            string name = _currentID.ID;
            var idPath = Path.Combine(_folderPath, name + "ID.asset");
            var configPath = Path.Combine(_folderPath, name + ".asset");
                
            AssetDatabase.CreateAsset(_currentID.ID, idPath);
            AssetDatabase.CreateAsset(_currentConfig.Config, configPath);
                
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            _currentBuildings = FindAllBuildings();
            SelectFromList(_currentBuildings.IndexOf(_currentConfig.Config));
            
            GUI.FocusControl("Name");
        }

        private void ApplyChanges()
        {
            _currentID.SO.Update();
            _currentID.PropName.stringValue = _bufferName;
            _currentID.SO.ApplyModifiedProperties();
            
            _currentConfig.SO.Update();
            _currentConfig.PropBaseCps.floatValue = _bufferBaseCps;
            _currentConfig.PropBaseCost.floatValue = _bufferBaseCost;
            _currentConfig.PropIcon.objectReferenceValue = _bufferIcon;
            _currentConfig.PropSilhouette.objectReferenceValue = _bufferSilhouette;
            _currentConfig.SO.ApplyModifiedProperties();

            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(_currentID.ID), _bufferName + "ID");
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(_currentConfig.Config), _bufferName);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            _hasPendingChanges = false;
            _currentBuildings = FindAllBuildings();
        }

        private void RevertChanges()
        {
            GUI.FocusControl(null);
            SetBuffersValues();
        }

        private void DeleteBuilding(BuildingConfig config)
        {
            if (!EditorUtility.DisplayDialog(
                    "Delete Building",
                    $"Are you sure you want to delete '{(string)config.buildingID}'?",
                    "Delete", "Cancel")) return;

            var configPath = AssetDatabase.GetAssetPath(config);
            var idPAth = AssetDatabase.GetAssetPath(config.buildingID);
            
            AssetDatabase.DeleteAsset(configPath);
            if (!string.IsNullOrEmpty(idPAth))
                AssetDatabase.DeleteAsset(idPAth);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            _currentBuildings = FindAllBuildings();
            DeselectFromList();
            _window.Repaint();
        }

        private void SetBuffersValues()
        {
            _bufferName = _currentID.PropName.stringValue;
            _bufferBaseCps = _currentConfig.PropBaseCps.floatValue;
            _bufferBaseCost = _currentConfig.PropBaseCost.floatValue;
            _bufferIcon = (Sprite)_currentConfig.PropIcon.objectReferenceValue;
            _bufferSilhouette = (Sprite)_currentConfig.PropSilhouette.objectReferenceValue;

            _hasPendingChanges = false;
        }

        private void LoadBuildingsInScene()
        {
            var bakery = Object.FindFirstObjectByType<Bakery>(FindObjectsInactive.Include);

            if (!bakery)
            {
                Debug.LogWarning("Couldn't find Bakery object in scene");
                return;
            }
            
            Undo.RecordObject(bakery, "Load Buildings");
            bakery.LoadBuildings(FindAllBuildings());
            EditorUtility.SetDirty(bakery);
        }
    }
}