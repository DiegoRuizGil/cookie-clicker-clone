using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class GrandmaUpgrade : IUpgrade
    {
        private readonly string _grandmaName;
        private readonly string _buildingName;
        private readonly float _grandmaEfficiencyMultiplier;
        private readonly Percentage _buildingMultiplier;
        private readonly int _grandmaGroupSize;

        private int _previousGrandmaAmount;
        private bool _grandmasUpgraded;
        
        public GrandmaUpgrade(string grandmaName, string buildingName, float grandmaEfficiencyMultiplier, Percentage buildingMultiplier, int grandmaGroupSize)
        {
            _grandmaName = grandmaName;
            _buildingName = buildingName;
            _grandmaEfficiencyMultiplier = grandmaEfficiencyMultiplier;
            _buildingMultiplier = buildingMultiplier;
            _grandmaGroupSize = grandmaGroupSize;

            _grandmasUpgraded = false;
        }
        
        public void Apply(CookieBaker baker)
        {
            var grandma = baker.FindBuilding(_grandmaName);
            var building = baker.FindBuilding(_buildingName);
            var currentGrandmaAmount = grandma?.Amount ?? 0;

            if (building == null || grandma == null) return;
            
            var grandmaGroups = GetGrandmaGroups(currentGrandmaAmount);
            if (grandmaGroups < 1) return;
            
            TryUpgradeGrandma(grandma);
            UpgradeBuilding(building, grandmaGroups);
            
            _previousGrandmaAmount = currentGrandmaAmount;
        }

        private void TryUpgradeGrandma(Building grandma)
        {
            if (_grandmasUpgraded) return;

            grandma.cps.AddEfficiency(_grandmaEfficiencyMultiplier);
            _grandmasUpgraded = true;
        }

        private void UpgradeBuilding(Building building, int grandmaGroups)
        {
            for (int i = 0; i < grandmaGroups; i++)
                building.cps.AddMultiplier(_buildingMultiplier);
        }
        
        private int GetGrandmaGroups(int currentGrandmaAmount)
        {
            var remainingGrandmas = _previousGrandmaAmount - ((_previousGrandmaAmount / _grandmaGroupSize) * _grandmaGroupSize);
            return (currentGrandmaAmount - _previousGrandmaAmount + remainingGrandmas) / _grandmaGroupSize;
        }
    }
}