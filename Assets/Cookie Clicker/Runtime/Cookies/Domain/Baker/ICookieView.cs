using System;

namespace Cookie_Clicker.Runtime.Cookies.Domain.Baker
{
    public interface ICookieView
    {
        void UpdateStats(double totalCookies, double cps);
        void AddCursors(int amount);
        void RemoveCursors(int amount);
        void Tap(double cookiesAmount);
        
        void RegisterTapListener(Action listener);
    }
}