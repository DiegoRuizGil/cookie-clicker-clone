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
            Settings, Buildings, Upgrades
        }
        
        [MenuItem("Tools/Cookie Assets")]
        public static void OpenWindow() => GetWindow<CookieAssetsTool>("Cookie Assets");

        private const string FolderPath = "Assets/Cookie Clicker/Data";

        private BuildingToolModule _buildingsModule;
        private UpgradeToolModule _upgradesModule;
        
        private ToolSection _currentToolSection = ToolSection.Buildings;

        private void OnEnable()
        {
            _buildingsModule = new BuildingToolModule(this, new BuildingRepository(FolderPath), FolderPath);
            _upgradesModule = new UpgradeToolModule(this, new UpgradeRepository(FolderPath), FolderPath);
        }

        private void OnGUI()
        {
            
            DrawTabs();

            switch (_currentToolSection)
            {
                case ToolSection.Settings:
                    break;
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
            var tabs = new string[] { "Settings", "Buildings", "Upgrades" };
            
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            _currentToolSection = (ToolSection)GUILayout.Toolbar((int)_currentToolSection, tabs, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
        }
    }
}