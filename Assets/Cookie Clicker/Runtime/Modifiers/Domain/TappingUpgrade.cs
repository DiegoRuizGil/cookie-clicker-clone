using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class TappingUpgrade : IUpgrade
    {
        private readonly Percentage _multiplier;
        
        public TappingUpgrade(Percentage multiplier)
        {
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker) => baker.tapping.AddMultiplier(_multiplier);
    }
}