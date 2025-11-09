using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions
{
    public interface IUnlockCondition
    {
        bool IsMet(CookieBaker baker);
    }
}