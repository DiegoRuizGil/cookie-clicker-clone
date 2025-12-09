using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEditor;

namespace Cookie_Clicker.Runtime.Tools.Editor.Buildings_Module
{
    public class BuildingRepository
    {
        private readonly string _folderPath;

        public BuildingRepository(string folderPath)
        {
            _folderPath = folderPath;
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
            return FindAll().Where(config =>
            {
                var name = ((string)config.buildingID).ToLower();
                return name.Contains(text.ToLower());
            }).ToList();
        }

        public void Delete(BuildingConfig building)
        {
            var buildingPath = AssetDatabase.GetAssetPath(building);
            var idPAth = AssetDatabase.GetAssetPath(building.buildingID);
            
            AssetDatabase.DeleteAsset(buildingPath);
            if (!string.IsNullOrEmpty(idPAth))
                AssetDatabase.DeleteAsset(idPAth);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}