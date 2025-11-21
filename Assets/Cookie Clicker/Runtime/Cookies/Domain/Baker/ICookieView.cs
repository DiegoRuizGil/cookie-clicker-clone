using System;

namespace Cookie_Clicker.Runtime.Cookies.Domain.Baker
{
    public interface ICookieView
    {
        void UpdateStats(float totalCookies, float cps);
        void AddCursors(int amount);
        void RemoveCursors(int amount);
        void Tap(float cookiesAmount);
        
        void RegisterTapListener(Action listener);
    }
}