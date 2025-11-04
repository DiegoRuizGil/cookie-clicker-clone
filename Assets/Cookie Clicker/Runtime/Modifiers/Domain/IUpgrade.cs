using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public interface IUpgrade
    {
        void Apply(CookieBaker cookieBaker);
    }
}