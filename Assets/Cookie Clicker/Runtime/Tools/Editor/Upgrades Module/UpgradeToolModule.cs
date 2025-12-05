using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cookie_Clicker.Runtime.Modifiers.Infrastructure;
using Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module
{
    public class UpgradeToolModule
    {
        private string _searchText;

        private Vector2 _scrollPos;
        private int _selectedIndex;
        private List<BaseUpgradeConfig> _currentUpgrades = new List<BaseUpgradeConfig>();
        
        private readonly EditorWindow _window;
        private readonly string _folderPath;
        private GenericMenu _upgradesCreationMenu;
        private readonly UpgradeEditorDrawer _upgradesDrawer;
        
        private static readonly GUIContent TrashIcon = EditorGUIUtility.IconContent("TreeEditor.Trash");
        private static readonly GUIContent PlusIcon = EditorGUIUtility.IconContent("Toolbar Plus More");

        public UpgradeToolModule(EditorWindow window, string folderPath)
        {
            _window = window;
            _folderPath = folderPath;
            _upgradesDrawer = new UpgradeEditorDrawer();
            
            InitUpgradesCreationMenu();
            _currentUpgrades = FindAllUpgrades();
            
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
                _upgradesCreationMenu.ShowAsContext();
            
            _searchText = GUILayout.TextField(_searchText, GUI.skin.FindStyle("ToolbarSearchTextField"));
            EditorGUILayout.EndHorizontal();
            
            using (var scroll = new EditorGUILayout.ScrollViewScope(_scrollPos, EditorStyles.helpBox))
            {
                _scrollPos = scroll.scrollPosition;
                
                for (int i = 0; i < _currentUpgrades.Count; i++)
                {
                    var rowRect = GUILayoutUtility.GetRect(0, 20, GUILayout.ExpandWidth(true));
                
                    var isSelected = i == _selectedIndex;
                    var bgColor = isSelected
                        ? ToolUtils.SelectedColor
                        : (i % 2 == 0) ? ToolUtils.DarkColor1 : ToolUtils.DarkColor2;
                    EditorGUI.DrawRect(rowRect, bgColor);
                    
                    GUI.Label(new Rect(rowRect.x + 5, rowRect.y, rowRect.width - 30, rowRect.height), _currentUpgrades[i].Name);

                    var iconRect = new Rect(rowRect.xMax - 20, rowRect.y + 2, 16, 16);

                    if (GUI.Button(iconRect, TrashIcon, GUIStyle.none))
                    {
                        DeleteUpgrade(_currentUpgrades[i]);
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
                LoadUpgradesInScene();
            
            EditorGUILayout.EndVertical();
        }

        private void DrawEditor()
        {
            _upgradesDrawer.Draw();
        }

        private void InitUpgradesCreationMenu()
        {
            _upgradesCreationMenu = new GenericMenu();
            foreach (var type in Enum.GetValues(typeof(UpgradeType)))
            {
                var content = new GUIContent(type + " upgrade");
                _upgradesCreationMenu.AddItem(content, false, () => CreateNewUpgrade((UpgradeType) type));
            }
        }

        private void CreateNewUpgrade(UpgradeType type)
        {
            _upgradesDrawer.SetDefaultOfType(type);

            var name = _upgradesDrawer.CurrentUpgrade.PropName.stringValue;
            var path = Path.Combine(_folderPath, name + ".asset");
            
            AssetDatabase.CreateAsset(_upgradesDrawer.CurrentUpgrade.Value, path);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            _currentUpgrades = FindAllUpgrades();
            SelectFromList(_currentUpgrades.IndexOf(_upgradesDrawer.CurrentUpgrade.Value));
            
            GUI.FocusControl("Name");
        }

        private void DeleteUpgrade(BaseUpgradeConfig upgrade)
        {
            if (!EditorUtility.DisplayDialog(
                    "Delete Upgrade",
                    $"Are you sure you want to delete '{upgrade.Name}'?",
                    "Delete", "Cancel")) return;

            var path = AssetDatabase.GetAssetPath(upgrade);
            AssetDatabase.DeleteAsset(path);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            _currentUpgrades = FindAllUpgrades();
            _selectedIndex = 0;
            _window.Repaint();
        }

        private List<BaseUpgradeConfig> FindAllUpgrades()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BaseUpgradeConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var upgrades = paths.Select(AssetDatabase.LoadAssetAtPath<BaseUpgradeConfig>).ToList();

            return upgrades;
        }

        private void SelectFromList(int index)
        {
            _selectedIndex = index;
            
            _upgradesDrawer.SetUpgrade(_currentUpgrades[index]);
        }
        
        private void LoadUpgradesInScene()
        {
            var upgradesComponent = Object.FindFirstObjectByType<UpgradesComponent>(FindObjectsInactive.Include);

            if (!upgradesComponent)
            {
                Debug.LogWarning("Couldn't find upgrades component");
                return;
            }
            
            Undo.RecordObject(upgradesComponent, "Load Upgrades");
            upgradesComponent.LoadUpgrades(FindAllUpgrades());
            EditorUtility.SetDirty(upgradesComponent);
        }
    }
}