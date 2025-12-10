using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public List<BaseUpgradeConfig> FindAll()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BaseUpgradeConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var upgrades = paths.Select(AssetDatabase.LoadAssetAtPath<BaseUpgradeConfig>).ToList();

            return upgrades;
        }

        public List<BaseUpgradeConfig> FindAllValid()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BaseUpgradeConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var upgrades = paths.Select(AssetDatabase.LoadAssetAtPath<BaseUpgradeConfig>)
                .Where(upgrade => upgrade.IsValid()).ToList();

            return upgrades;
        }

        public List<BaseUpgradeConfig> FindByName(string text)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BaseUpgradeConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var upgrades = paths.Select(AssetDatabase.LoadAssetAtPath<BaseUpgradeConfig>)
                .Where(upgrade =>
                {
                    var name = upgrade.Name.ToLower();
                    return name.Contains(text.ToLower());
                }).ToList();

            return upgrades;
        }

        public List<BaseUpgradeConfig> FindByBuilding(string buildingName)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BaseUpgradeConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var upgrades = paths.Select(AssetDatabase.LoadAssetAtPath<BaseUpgradeConfig>)
                .Where(upgrade => upgrade.GetAssociatedBuildingIDs().Contains(buildingName)).ToList();

            return upgrades;
        }
    }
}