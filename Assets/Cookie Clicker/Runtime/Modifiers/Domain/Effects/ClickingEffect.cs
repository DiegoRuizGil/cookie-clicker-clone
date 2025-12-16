using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Effects
{
    public class ClickingEffect : IUpgradeEffect
    {
        private readonly Percentage _multiplier;
        
        public ClickingEffect(Percentage multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker) => baker.tapping.AddMultiplier(_multiplier);
    }
}