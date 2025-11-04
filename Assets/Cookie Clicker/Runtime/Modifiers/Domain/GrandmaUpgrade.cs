using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class GrandmaUpgrade : IUpgrade
    {
        private readonly string _grandmaName;
        private readonly string _buildingName;
        private readonly float _grandmaEfficiencyMultiplier;
        private readonly float _buildingMultiplier;
        private readonly int _grandmaCount;

        private int _previousGrandmaAmount;
        
        public GrandmaUpgrade(string grandmaName, string buildingName, float grandmaEfficiencyMultiplier, float buildingMultiplier, int grandmaCount)
        {
            _grandmaName = grandmaName;
            _buildingName = buildingName;
            _grandmaEfficiencyMultiplier = grandmaEfficiencyMultiplier;
            _buildingMultiplier = buildingMultiplier;
            _grandmaCount = grandmaCount;
        }
        
        public void Apply(CookieBaker baker)
        {
            var grandma = baker.FindBuilding(_grandmaName);
            var building = baker.FindBuilding(_buildingName);
            var currentGrandmaAmount = grandma?.Quantity ?? 0;

            if (building == null || grandma == null) return;

            _previousGrandmaAmount = grandma.Quantity;
        }
    }
}