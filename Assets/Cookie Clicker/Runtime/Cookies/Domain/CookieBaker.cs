using System;
using System.Collections.Generic;
using System.Linq;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CookieBaker
    {
        public readonly List<Building> buildings = new List<Building>();
        public int Production => buildings.Sum(b => b.Production);
        public int TotalCookies { get; private set; }

        public void PassTime(TimeSpan delta)
        {
            TotalCookies += (int)(Production * delta.TotalSeconds);
        }

        public int GetProduction()
        {
            return buildings.Sum(building => building.Production);
        }
    }
}