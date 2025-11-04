using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class GlobalProductionUpgrade : IUpgrade
    {
        private readonly float _multiplier;
        
        public GlobalProductionUpgrade(float multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker) => baker.productionMultiplier += _multiplier;
    }
}