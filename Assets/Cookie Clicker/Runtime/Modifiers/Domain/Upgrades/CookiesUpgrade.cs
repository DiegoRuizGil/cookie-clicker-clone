using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades
{
    public class CookiesUpgrade : Upgrade
    {
        private readonly Percentage _multiplier;
        
        public CookiesUpgrade(Percentage multiplier)
        {
            _multiplier = multiplier;
        }
        
        public override void Apply(CookieBaker baker) => baker.ProductionMultiplier += _multiplier;
    }
}