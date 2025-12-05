using Cookie_Clicker.Runtime.Modifiers.Infrastructure;
using UnityEditor;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module
{
    public class UpgradeConfigWrapper
    {
        public SerializedObject SO { get; private set; }
        public SerializedProperty PropName { get; private set; }
        public SerializedProperty PropIcon { get; private set; }
        public SerializedProperty PropCost { get; private set; }
        public SerializedProperty PropDescription { get; private set; }

        private BaseUpgradeConfig _config;
        public UpgradeType Type => _config.Type;
        
        public void Set(BaseUpgradeConfig config)
        {
            _config = config;
            
            SO = new SerializedObject(config);
            PropName = SO.FindProperty("upgradeName");
            PropIcon = SO.FindProperty("icon");
            PropCost = SO.FindProperty("cost");
            PropDescription = SO.FindProperty("description");
        }
    }
}