using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions
{
    public class CookiesBakedCondition : IUnlockCondition
    {
        private readonly double _requiredCookies;
        
        public CookiesBakedCondition(double requiredCookies)
        {
            _requiredCookies = requiredCookies;
        }

        public bool IsMet(CookieBaker baker) => baker.BakedCookies >= _requiredCookies;
    }
}