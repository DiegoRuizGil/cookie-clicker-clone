using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class TappingUpgrade : IUpgrade
    {
        private readonly float _multiplier;
        
        public TappingUpgrade(float multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker) => baker.tapMultiplier += _multiplier;
    }
}