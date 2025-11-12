using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades
{
    public class TappingCursorUpgrade : Upgrade
    {
        private readonly string _cursorName;
        private readonly float _efficiencyMultiplier;
        
        public TappingCursorUpgrade(string cursorName, float efficiencyMultiplier)
        {
            _cursorName = cursorName;
            _efficiencyMultiplier = efficiencyMultiplier;
        }
        
        public override void Apply(CookieBaker baker)
        {
            var cursor = baker.FindBuilding(_cursorName);
            if (cursor != null)
            {
                baker.tapping.AddEfficiency(_efficiencyMultiplier);
                cursor.cps.AddEfficiency(_efficiencyMultiplier);
            }
        }
    }
}