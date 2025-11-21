using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions
{
    public class CookiesBakedCondition : IUnlockCondition
    {
        private readonly int _requiredCookies;
        
        public CookiesBakedCondition(int requiredCookies)
        {
            _requiredCookies = requiredCookies;
        }

        public bool IsMet(CookieBaker baker) => baker.BakedCookies >= _requiredCookies;
    }
}