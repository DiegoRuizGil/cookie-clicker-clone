using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions
{
    public class BuildingCountCondition : IUnlockCondition
    {
        private readonly string _buildingName;
        private readonly int _requiredAmount;

        public BuildingCountCondition(string buildingName, int requiredAmount)
        {
            _buildingName = buildingName;
            _requiredAmount = requiredAmount;
        }
        
        public bool IsMet(CookieBaker baker) => baker.OwnedBuildingsOf(_buildingName) >= _requiredAmount;
    }
}