using System;
using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Modifiers.Infrastructure;
using Cookie_Clicker.Runtime.Tools.Editor.Buildings_Module;
using Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module
{
    public class UpgradeToolModule
    {
        private string _searchText = "";

        private Vector2 _drawerScrollPos;
        
        private Vector2 _listScrollPos;
        private int _selectedIndex;
        private bool Deselected => _selectedIndex < 0;
        private List<BaseUpgradeConfig> _currentUpgrades;

        private bool _hasPendingChanges;
        
        private readonly EditorWindow _window;
        private readonly UpgradeRepository _upgradeRepository;
        private readonly BuildingRepository _buildingRepository;
        private readonly UpgradeEditorDrawer _upgradesDrawer;
        private readonly SelectPopup<UpgradeType> _typeFilter;
        private readonly SelectPopup<string> _buildingFilter;
        private GenericMenu _upgradesCreationMenu;
        
        private List<UpgradeType> _selectedTypes = new List<UpgradeType>();
        private List<string> _selectedBuildings = new List<string>();

        public UpgradeToolModule(EditorWindow window, UpgradeRepository upgradeRepository, BuildingRepository buildingRepository)
        {
            _window = window;
            _upgradeRepository = upgradeRepository;
            _buildingRepository = buildingRepository;
            _upgradesDrawer = new UpgradeEditorDrawer();

            _typeFilter = new SelectPopup<UpgradeType>(
                Enum.GetValues(typeof(UpgradeType)).Cast<UpgradeType>().ToList(),
                Enum.GetValues(typeof(UpgradeType)).Cast<UpgradeType>().ToList(),
                newValue =>
                {
                    _selectedTypes =  newValue;
                    SearchUpgrades();
                });
            _buildingFilter = new SelectPopup<string>(
                GetBuildingsName(), GetBuildingsName(), newValue =>
                {
                    _selectedBuildings = newValue;
                    SearchUpgrades();
                });
            
            InitUpgradesCreationMenu();
            InitList();
        }

        public void OnGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                DrawList();
                DrawEditor();
            }
        }

        public void SearchUpgrades()
        {
            var upgrades = _upgradeRepository.FindAll();

            if (!string.IsNullOrEmpty(_searchText))
                upgrades = upgrades.Where(u => u.Name.ToLower().Contains(_searchText.ToLower())).ToList();

            if (_selectedTypes.Count > 0)
                upgrades = upgrades.Where(u => _selectedTypes.Contains(u.Type)).ToList();

            if (_selectedBuildings.Count == 1)
                upgrades = upgrades.Where(u => u.GetAssociatedBuildingIDs().Any(b => _selectedBuildings.Contains(b))).ToList();

            _currentUpgrades = upgrades;
            
            if (_currentUpgrades.Count == 0)
                DeselectFromList();
            else
                SelectFromList(0);
        }

        public void UpdateBuildingsNameList()
        {
            _buildingFilter.SetOptions(GetBuildingsName());
        }

        private List<string> GetBuildingsName() => _buildingRepository.FindAll().Select(b => (string)b.buildingID).ToList();
        
        private void InitList()
        {
            _currentUpgrades = _upgradeRepository.FindAll();

            if (_currentUpgrades.Count == 0)
                DeselectFromList();
            else
                SelectFromList(_selectedIndex);
        }

        private void DrawList()
        {
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(200));
            
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            if (GUILayout.Button(ToolUtils.PlusMoreIcon, EditorStyles.toolbarButton, GUILayout.Width(25)))
                _upgradesCreationMenu.ShowAsContext();

            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                _searchText = GUILayout.TextField(_searchText, GUI.skin.FindStyle("ToolbarSearchTextField"));
                if (changeCheck.changed)
                    SearchUpgrades();
            }
            EditorGUILayout.EndHorizontal();
            
            _typeFilter.Draw("Type");
            _buildingFilter.Draw("Building");
            
            using (var scroll = new EditorGUILayout.ScrollViewScope(_listScrollPos, EditorStyles.helpBox))
            {
                _listScrollPos = scroll.scrollPosition;
                
                for (int i = 0; i < _currentUpgrades.Count; i++)
                {
                    var rowRect = GUILayoutUtility.GetRect(0, 20, GUILayout.ExpandWidth(true));
                
                    var isSelected = i == _selectedIndex;
                    var bgColor = isSelected
                        ? ToolUtils.SelectedColor
                        : (i % 2 == 0) ? ToolUtils.DarkColor1 : ToolUtils.DarkColor2;
                    EditorGUI.DrawRect(rowRect, bgColor);
                    
                    GUI.Label(new Rect(rowRect.x + 5, rowRect.y, rowRect.width - 30, rowRect.height), _currentUpgrades[i].Name);
                    
                    if (!_currentUpgrades[i].IsValid())
                    {
                        var warningRect = new Rect(rowRect.xMax - 40, rowRect.y + 2, 16, 16);
                        var warningContent = new GUIContent(ToolUtils.WarningIcon)
                        {
                            tooltip = "This upgrade has no building assigned!"
                        };
                        GUI.Label(warningRect, warningContent, GUIStyle.none);
                    }
                    
                    var deleteRect = new Rect(rowRect.xMax - 20, rowRect.y + 2, 16, 16);
                    if (GUI.Button(deleteRect, ToolUtils.TrashIcon, GUIStyle.none))
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
            if (Deselected) return;

            _drawerScrollPos =  EditorGUILayout.BeginScrollView(_drawerScrollPos);
            EditorGUILayout.BeginVertical();
            
            EditorGUI.BeginChangeCheck();
            _upgradesDrawer.Draw();
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
            EditorGUILayout.EndScrollView();
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

            _upgradeRepository.CreateAsset(_upgradesDrawer.CurrentUpgrade.Value);

            SearchUpgrades();
            SelectFromList(_currentUpgrades.IndexOf(_upgradesDrawer.CurrentUpgrade.Value));
            
            GUI.FocusControl("Name");
        }

        private void DeleteUpgrade(BaseUpgradeConfig upgrade)
        {
            if (!EditorUtility.DisplayDialog(
                    "Delete Upgrade",
                    $"Are you sure you want to delete '{upgrade.Name}'?\n\nThis action cannot be undone.",
                    "Delete", "Cancel")) return;

            _upgradeRepository.DeleteAsset(upgrade);
            
            SearchUpgrades();
            DeselectFromList();
            _window.Repaint();
        }

        private void ApplyChanges()
        {
            _upgradesDrawer.ApplyChanges();

            if (_upgradesDrawer.NeedsToBeRenamed)
            {
                _upgradeRepository.RenameAsset(_upgradesDrawer.CurrentUpgrade.Value, _upgradesDrawer.CurrentUpgrade.Value.Name);
                _upgradesDrawer.NeedsToBeRenamed = false;
            }
            
            _hasPendingChanges = false;
            SearchUpgrades();
            SelectFromList(_currentUpgrades.IndexOf(_upgradesDrawer.CurrentUpgrade.Value));
        }

        private void RevertChanges()
        {
            _upgradesDrawer.RevertChanges();
            _hasPendingChanges = false;
        }
        
        private void SetBufferValues()
        {
            _upgradesDrawer.SetBufferValues();
            _hasPendingChanges = false;
        }

        private void SelectFromList(int index)
        {
            _selectedIndex = index;
            
            // var upgrade = index < _currentUpgrades.Count ? _currentUpgrades[index] : null;
            if (index >= _currentUpgrades.Count) return;
            _upgradesDrawer.CurrentUpgrade.Set(_currentUpgrades[index]);
            SetBufferValues();
        }

        private void DeselectFromList()
        {
            _selectedIndex = -1;
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
            upgradesComponent.LoadUpgrades(_upgradeRepository.FindAllValid());
            EditorUtility.SetDirty(upgradesComponent);
        }
    }
}