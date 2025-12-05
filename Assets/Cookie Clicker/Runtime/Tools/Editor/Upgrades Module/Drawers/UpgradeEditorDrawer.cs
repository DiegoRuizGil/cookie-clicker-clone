using System.Collections.Generic;
using Cookie_Clicker.Runtime.Modifiers.Infrastructure;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class UpgradeEditorDrawer
    {
        public UpgradeConfigWrapper CurrentUpgrade { get; } = new UpgradeConfigWrapper();
        
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
            _specificFieldsDrawers[CurrentUpgrade.Type].Draw();
            
            EditorGUILayout.EndVertical();
        }

        public void SetUpgrade(BaseUpgradeConfig upgrade)
        {
            CurrentUpgrade.Set(upgrade);

            SetBufferValues();
        }

        public void SetDefaultOfType(UpgradeType type)
        {
            BaseUpgradeConfig upgrade = type switch
            {
                UpgradeType.Tiered => ScriptableObject.CreateInstance<EfficiencyUpgradeConfig>(),
                UpgradeType.Cursor => ScriptableObject.CreateInstance<TappingCursorUpgradeConfig>(),
                UpgradeType.Grandma => ScriptableObject.CreateInstance<GrandmaUpgradeConfig>(),
                UpgradeType.Clicking => ScriptableObject.CreateInstance<TappingUpgradeConfig>(),
                UpgradeType.Cookies => ScriptableObject.CreateInstance<CookiesUpgradeConfig>(),
                _ => ScriptableObject.CreateInstance<EfficiencyUpgradeConfig>(),
            };
            
            CurrentUpgrade.Set(upgrade);
            
            CurrentUpgrade.SO.Update();
            CurrentUpgrade.PropName.stringValue = $"New {type} Upgrade";
            CurrentUpgrade.SO.ApplyModifiedProperties();
        }

        private void SetBufferValues()
        {
            _generalFieldsDrawer.SetBufferValues(CurrentUpgrade);
            _specificFieldsDrawers[CurrentUpgrade.Type].SetBufferValues(CurrentUpgrade);
        }
    }
}