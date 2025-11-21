using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Effects
{
    public class TappingEffect : IUpgradeEffect
    {
        private readonly Percentage _multiplier;
        
        public TappingEffect(Percentage multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker) => baker.tapping.AddMultiplier(_multiplier);
    }
}