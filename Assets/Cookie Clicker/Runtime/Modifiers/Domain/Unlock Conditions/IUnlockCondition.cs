using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions
{
    public interface IUnlockCondition
    {
        bool IsMet(CookieBaker baker);
    }
}