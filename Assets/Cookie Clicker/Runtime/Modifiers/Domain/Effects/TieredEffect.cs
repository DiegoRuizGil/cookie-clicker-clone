using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Effects
{
    public class TieredEffect : IUpgradeEffect
    {
        private readonly string _buildingName;
        private readonly float _multiplier;
        
        public TieredEffect(string buildingName, float multiplier)
        {
            _buildingName = buildingName;
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker)
        {
            var building = baker.FindBuilding(_buildingName);
            if (building != null)
                building.cps.AddEfficiency(_multiplier);
        }
    }
}