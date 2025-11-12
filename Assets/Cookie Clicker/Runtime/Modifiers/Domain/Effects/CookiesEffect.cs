using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Effects
{
    public class CookiesEffect : IUpgradeEffect
    {
        private readonly Percentage _multiplier;
        
        public CookiesEffect(Percentage multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker) => baker.ProductionMultiplier += _multiplier;
    }
}