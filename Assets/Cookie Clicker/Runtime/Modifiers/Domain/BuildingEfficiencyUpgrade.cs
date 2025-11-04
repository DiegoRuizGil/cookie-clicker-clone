using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class BuildingEfficiencyUpgrade : IUpgrade
    {
        private readonly string _buildingName;
        private readonly float _multiplier;
        
        public BuildingEfficiencyUpgrade(string buildingName, float multiplier)
        {
            _buildingName = buildingName;
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker)
        {
            var building = baker.buildings.Find(b => b.Name == _buildingName);
            if (building != null)
                building.cpsMultiplier *= _multiplier;
        }
    }
}