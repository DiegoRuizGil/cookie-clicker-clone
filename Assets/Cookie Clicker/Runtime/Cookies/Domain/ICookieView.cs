using System;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public interface ICookieView
    {
        void UpdateStats(float totalCookies, float cps);
        void RegisterListener(Action callback);
    }
}