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
        
        public void RenameAsset(BaseUpgradeConfig upgrade, string name)
        {
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(upgrade), name);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void DeleteAsset(BaseUpgradeConfig upgrade)
        {
            var path = AssetDatabase.GetAssetPath(upgrade);
            AssetDatabase.DeleteAsset(path);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}