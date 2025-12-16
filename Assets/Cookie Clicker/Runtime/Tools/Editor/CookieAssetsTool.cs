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
            var upgradesPath = $"{BasePath}/Upgrades";
            var buildingsPath = $"{BasePath}/Buildings";
            CreateFolders(upgradesPath, buildingsPath);
            
            var upgradeRepository = new UpgradeRepository(upgradesPath);
            var buildingRepository = new BuildingRepository(buildingsPath, upgradeRepository);
            
            _upgradesModule = new UpgradeToolModule(this, upgradeRepository, buildingRepository.FindAll().Select(b => (string)b.buildingID).ToList());
            _buildingsModule = new BuildingToolModule(this, buildingRepository);
        }

        private void CreateFolders(params string[] paths)
        {
            foreach (var path in paths)
                EnsureFolderExists(path);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void EnsureFolderExists(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;

            var parts = path.Split('/');
            string currentPath = parts[0];
            
            for (var i = 1; i < parts.Length; i++)
            {
                var nextPath = $"{currentPath}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(nextPath))
                    AssetDatabase.CreateFolder(currentPath, parts[i]);

                currentPath = nextPath;
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
                            _buildingsModule.SearchBuildings();
                            break;
                        case ToolSection.Upgrades:
                            _upgradesModule.SearchUpgrades();
                            break;
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}