using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Buildings_Module
{
    public class BuildingRepository
    {
        private readonly string _folderPath;
        private readonly UpgradeRepository _upgradeRepository;

        public BuildingRepository(string folderPath, UpgradeRepository upgradeRepository)
        {
            _folderPath = folderPath;
            _upgradeRepository = upgradeRepository;
        }

        public void CreateAsset(BuildingConfig building)
        {
            string name = building.buildingID;
            var idPath = Path.Combine(_folderPath, name + "ID.asset");
            var buildingPath = Path.Combine(_folderPath, name + ".asset");
                
            AssetDatabase.CreateAsset(building.buildingID, idPath);
            AssetDatabase.CreateAsset(building, buildingPath);
                
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void RenameAsset(BuildingConfig building, string name)
        {
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(building.buildingID), name + "ID");
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(building), name);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void DeleteAsset(BuildingConfig building)
        {
            var associatedUpgrades = _upgradeRepository.FindByBuilding(building.buildingID);
            foreach (var upgrade in associatedUpgrades)
                _upgradeRepository.DeleteAsset(upgrade);
            
            var buildingPath = AssetDatabase.GetAssetPath(building);
            var idPAth = AssetDatabase.GetAssetPath(building.buildingID);
            
            AssetDatabase.DeleteAsset(buildingPath);
            if (!string.IsNullOrEmpty(idPAth))
                AssetDatabase.DeleteAsset(idPAth);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public List<BuildingConfig> FindAll()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BuildingConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var buildings = paths.Select(AssetDatabase.LoadAssetAtPath<BuildingConfig>)
                .Where(config => config.buildingID).OrderBy(config => config.BaseCost).ToList();
            
            return buildings;
        }

        public List<BuildingConfig> FindByName(string text)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(BuildingConfig)}", new[] { _folderPath });
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            var buildings = paths.Select(AssetDatabase.LoadAssetAtPath<BuildingConfig>)
                .Where(config => config.buildingID)
                .Where(config =>
                {
                    var name = ((string)config.buildingID).ToLower();
                    return name.Contains(text.ToLower());
                })
                .OrderBy(config => config.BaseCost).ToList();
            
            return buildings;
        }
    }
}