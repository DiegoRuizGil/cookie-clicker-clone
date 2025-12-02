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
            GUILayout.BeginArea(new Rect(10, 10, position.width - 20, position.height - 20));
            
            using (new EditorGUILayout.HorizontalScope())
            {
                _buildingModule.DrawList();
                _buildingModule.DrawEditor();
            }

            if (Event.current.type == EventType.MouseDown)
            {
                GUI.FocusControl(null);
                Repaint();
            }
            
            GUILayout.EndArea();
        }
    }
}