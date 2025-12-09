using System.IO;
using Cookie_Clicker.Runtime.Modifiers.Infrastructure;
using UnityEditor;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module
{
    public class UpgradeRepository
    {
        private readonly string _folderPath;
        
        public UpgradeRepository(string folderPath)
        {
            _folderPath = folderPath;
        }
        
        public void CreateAsset(BaseUpgradeConfig upgrade)
        {
            var name = upgrade.Name;
            var upgradePath = Path.Combine(_folderPath, name + ".asset");
                
            AssetDatabase.CreateAsset(upgrade, upgradePath);
                
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}