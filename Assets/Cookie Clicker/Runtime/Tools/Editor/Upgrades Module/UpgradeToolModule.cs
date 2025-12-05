using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Modifiers.Infrastructure;
using Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers;
using UnityEditor;
using UnityEngine;

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
        private readonly GenericMenu _upgradesMenu;
        private readonly UpgradeEditorDrawer _upgradesDrawer;
        
        private static readonly GUIContent TrashIcon = EditorGUIUtility.IconContent("TreeEditor.Trash");
        private static readonly GUIContent PlusIcon = EditorGUIUtility.IconContent("Toolbar Plus More");

        public UpgradeToolModule(EditorWindow window, string folderPath)
        {
            _window = window;
            _folderPath = folderPath;
            _upgradesDrawer = new UpgradeEditorDrawer();
            
            _currentUpgrades = FindAllUpgrades();
            
            _upgradesMenu = new GenericMenu();
            _upgradesMenu.AddItem(new GUIContent(nameof(UpgradeType.Tiered) + " upgrade"), false, () => Debug.Log("Tiered upgrade"));
            _upgradesMenu.AddItem(new GUIContent(nameof(UpgradeType.Cursor) + " upgrade"), false, () => Debug.Log("Cursor upgrade"));
            _upgradesMenu.AddItem(new GUIContent(nameof(UpgradeType.Grandma) + " upgrade"), false, () => Debug.Log("Grandma upgrade"));
            _upgradesMenu.AddItem(new GUIContent(nameof(UpgradeType.Clicking) + " upgrade"), false, () => Debug.Log("Clicking upgrade"));
            _upgradesMenu.AddItem(new GUIContent(nameof(UpgradeType.Cookies) + " upgrade"), false, () => Debug.Log("Cookies upgrade"));
            
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
                _upgradesMenu.ShowAsContext();
            
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
                        // DeleteBuilding(_currentUpgrades[i]);
                        Debug.Log($"Deleting {_currentUpgrades[i].Name}");
                        break;
                    }

                    if (Event.current.type == EventType.MouseDown && rowRect.Contains(Event.current.mousePosition))
                    {
                        SelectFromList(i);
                        // _selectedIndex = i;
                        _window.Repaint();
                    }
                }
            }
            
            EditorGUILayout.EndVertical();
        }

        private void DrawEditor()
        {
            _upgradesDrawer.Draw();
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
    }
}