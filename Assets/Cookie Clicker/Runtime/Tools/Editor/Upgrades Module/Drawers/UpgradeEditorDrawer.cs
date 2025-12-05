using System.Collections.Generic;
using Cookie_Clicker.Runtime.Modifiers.Infrastructure;
using UnityEditor;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class UpgradeEditorDrawer
    {
        private readonly UpgradeConfigWrapper _currentUpgrade = new UpgradeConfigWrapper();
        
        private readonly GeneralFieldsDrawer _generalFieldsDrawer = new GeneralFieldsDrawer();

        private readonly Dictionary<UpgradeType, IUpgradeFieldsDrawer> _specificFieldsDrawers = new()
        {
            { UpgradeType.Tiered, new TieredFieldsDrawer() },
            { UpgradeType.Cursor, new CursorFieldsDrawer() },
            { UpgradeType.Grandma, new GrandmaFieldsDrawer() },
            { UpgradeType.Clicking, new ClickingFieldsDrawer() },
            { UpgradeType.Cookies, new CookiesFieldsDrawer() },
        };
        
        public void Draw()
        {
            EditorGUILayout.BeginVertical();
            
            _generalFieldsDrawer.Draw();
            _specificFieldsDrawers[_currentUpgrade.Type].Draw();
            
            EditorGUILayout.EndVertical();
        }

        public void SetUpgrade(BaseUpgradeConfig upgrade)
        {
            _currentUpgrade.Set(upgrade);

            SetBufferValues();
        }

        private void SetBufferValues()
        {
            _generalFieldsDrawer.SetBufferValues(_currentUpgrade);
            _specificFieldsDrawers[_currentUpgrade.Type].SetBufferValues(_currentUpgrade);
        }
    }
}