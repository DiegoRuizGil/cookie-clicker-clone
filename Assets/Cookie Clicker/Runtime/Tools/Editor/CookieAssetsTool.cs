using System.IO;
using System.Linq;
using Cookie_Clicker.Runtime.Tools.Editor.Buildings_Module;
using Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor
{
    public class CookieAssetsTool : EditorWindow
    {
        private enum ToolSection
        {
            Buildings, Upgrades
        }
        
        [MenuItem("Tools/Cookie Assets")]
        public static void OpenWindow() => GetWindow<CookieAssetsTool>("Cookie Assets");

        private const string BasePath = "Assets/Cookie Clicker/Data";

        private BuildingToolModule _buildingsModule;
        private UpgradeToolModule _upgradesModule;
        
        private ToolSection _currentToolSection = ToolSection.Buildings;

        private void OnEnable()
        {
            var upgradesPath = Path.Combine(BasePath, "Upgrades");
            var buildingsPath = Path.Combine(BasePath, "Buildings");
            
            CreateFolders(upgradesPath, buildingsPath);
            
            var upgradeRepository = new UpgradeRepository(upgradesPath);
            var buildingRepository = new BuildingRepository(buildingsPath, upgradeRepository);
            
            _upgradesModule = new UpgradeToolModule(this, upgradeRepository, buildingRepository.FindAll().Select(b => (string)b.buildingID).ToList());
            _buildingsModule = new BuildingToolModule(this, buildingRepository);
        }

        private void CreateFolders(params string[] paths)
        {
            foreach (var path in paths)
            {
                // if (!AssetDatabase.IsValidFolder(path))
                //     AssetDatabase.CreateFolder(path);
            }
        }

        private void OnGUI()
        {
            
            DrawTabs();

            switch (_currentToolSection)
            {
                case ToolSection.Buildings:
                    _buildingsModule.OnGUI();
                    break;
                case ToolSection.Upgrades:
                    _upgradesModule.OnGUI();
                    break;
            }
            
            if (Event.current.type == EventType.MouseDown)
            {
                GUI.FocusControl(null);
                Repaint();
            }
        }

        private void DrawTabs()
        {
            var tabs = new string[] { "Buildings", "Upgrades" };
            
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                _currentToolSection = (ToolSection)GUILayout.Toolbar((int)_currentToolSection, tabs, GUILayout.ExpandWidth(true));
                if (changeCheck.changed)
                {
                    switch (_currentToolSection)
                    {
                        case ToolSection.Buildings:
                            _buildingsModule.UpdateList();
                            break;
                        case ToolSection.Upgrades:
                            _upgradesModule.UpdateList();
                            break;
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}