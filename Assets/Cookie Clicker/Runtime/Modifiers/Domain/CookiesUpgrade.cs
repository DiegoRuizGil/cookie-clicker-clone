using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class CookiesUpgrade : IUpgrade
    {
        private readonly Percentage _multiplier;
        
        public CookiesUpgrade(Percentage multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker) => baker.ProductionMultiplier += _multiplier;
    }
}