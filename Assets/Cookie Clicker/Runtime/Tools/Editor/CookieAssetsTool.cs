using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor
{
    public class CookieAssetsTool : EditorWindow
    {
        [MenuItem("Tools/Cookie Assets")]
        public static void OpenWindow() => GetWindow<CookieAssetsTool>("Cookie Assets");

        private const string FolderPath = "Assets/Cookie Clicker/Data";

        private BuildingToolModule _buildingModule;

        private void OnEnable()
        {
            _buildingModule = new BuildingToolModule(this, FolderPath);
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, position.width, position.height - 20));
            
            _buildingModule.OnGUI();

            if (Event.current.type == EventType.MouseDown)
            {
                GUI.FocusControl(null);
                Repaint();
            }
            
            GUILayout.EndArea();
        }
    }
}