using System;
using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CookieBaker
    {
        public readonly List<Building> buildings = new List<Building>();
        
        public int TotalCookies { get; private set; }

        public void PassTime(TimeSpan delta)
        {
            
        }
    }
}