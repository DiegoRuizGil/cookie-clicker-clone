using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions
{
    public class HandMadeCookiesCondition : IUnlockCondition
    {
        private readonly double _requiredCookies;
        
        public HandMadeCookiesCondition(double requiredCookies)
        {
            _requiredCookies = requiredCookies;
        }
        
        public bool IsMet(CookieBaker baker) => baker.HandMadeCookies >= _requiredCookies;
    }
}