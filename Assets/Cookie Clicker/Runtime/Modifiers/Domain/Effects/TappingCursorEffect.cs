using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Effects
{
    public class TappingCursorEffect : IUpgradeEffect
    {
        private readonly string _cursorName;
        private readonly float _efficiencyMultiplier;
        
        public TappingCursorEffect(string cursorName, float efficiencyMultiplier)
        {
            _cursorName = cursorName;
            _efficiencyMultiplier = efficiencyMultiplier;
        }
        
        public void Apply(CookieBaker baker)
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