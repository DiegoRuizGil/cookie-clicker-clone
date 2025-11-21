using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Effects
{
    public interface IUpgradeEffect
    {
        void Apply(CookieBaker baker);
    }
}