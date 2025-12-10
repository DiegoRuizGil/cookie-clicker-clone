using System.Collections.Generic;
using Cookie_Clicker.Runtime.Modifiers.Infrastructure;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class UpgradeEditorDrawer
    {
        public UpgradeConfigWrapper CurrentUpgrade { get; } = new UpgradeConfigWrapper();

        private bool _hasPendingChanges;
        
        private readonly GeneralFieldsDrawer _generalFieldsDrawer = new GeneralFieldsDrawer();
        private readonly Dictionary<UpgradeType, IUpgradeFieldsDrawer> _specificFieldsDrawers = new()
        {
            { UpgradeType.Tiered, new TieredFieldsDrawer() },
            { UpgradeType.Cursor, new CursorFieldsDrawer() },
            { UpgradeType.Grandma, new GrandmaFieldsDrawer() },
            { UpgradeType.Clicking, new ClickingFieldsDrawer() },
            { UpgradeType.Cookies, new CookiesFieldsDrawer() },
        };

        private readonly UpgradeRepository _upgradeRepository;
        
        public UpgradeEditorDrawer(UpgradeRepository repository)
        {
            _upgradeRepository = repository;
        }
        
        public void Draw()
        {
            EditorGUILayout.BeginVertical();
            
            EditorGUI.BeginChangeCheck();
            _generalFieldsDrawer.Draw();
            _specificFieldsDrawers[CurrentUpgrade.Type].Draw();

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
                UpgradeType.Tiered => ScriptableObject.CreateInstance<TieredUpgradeConfig>(),
                UpgradeType.Cursor => ScriptableObject.CreateInstance<CursorUpgradeConfig>(),
                UpgradeType.Grandma => ScriptableObject.CreateInstance<GrandmaUpgradeConfig>(),
                UpgradeType.Clicking => ScriptableObject.CreateInstance<ClickingUpgradeConfig>(),
                UpgradeType.Cookies => ScriptableObject.CreateInstance<CookiesUpgradeConfig>(),
                _ => ScriptableObject.CreateInstance<TieredUpgradeConfig>(),
            };
            
            CurrentUpgrade.Set(upgrade);
            
            CurrentUpgrade.SO.Update();
            CurrentUpgrade.PropName.stringValue = $"New {type} Upgrade";
            CurrentUpgrade.SO.ApplyModifiedProperties();
        }

        private void ApplyChanges()
        {
            _generalFieldsDrawer.ApplyChanges(CurrentUpgrade);
            _specificFieldsDrawers[CurrentUpgrade.Type].ApplyChanges(CurrentUpgrade);

            if (_generalFieldsDrawer.needsToBeRenamed)
            {
                _upgradeRepository.RenameAsset(CurrentUpgrade.Value, CurrentUpgrade.Value.Name);
                _generalFieldsDrawer.needsToBeRenamed = false;
            }
            
            _hasPendingChanges = false;
        }

        private void RevertChanges()
        {
            GUI.FocusControl(null);
            SetBufferValues();
        }

        private void SetBufferValues()
        {
            _generalFieldsDrawer.SetBufferValues(CurrentUpgrade);
            _specificFieldsDrawers[CurrentUpgrade.Type].SetBufferValues(CurrentUpgrade);

            _hasPendingChanges = false;
        }
    }
}