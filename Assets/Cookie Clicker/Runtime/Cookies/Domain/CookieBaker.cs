using System;
using System.Collections.Generic;
using System.Linq;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CookieBaker
    {
        public readonly List<Building> buildings = new List<Building>();
        public float Production => buildings.Sum(b => b.Production) * (1 + ProductionMultiplier);
        public float CookiePerTap => tapping.Base * tapping.Efficiency + (Production * tapping.Multiplier);
        public int TotalCookies { get; private set; }

        public Percentage ProductionMultiplier { get; set; } = Percentage.Zero();
        public readonly ProductionStat tapping = new ProductionStat(1);

        public void PassTime(TimeSpan delta)
        {
            TotalCookies += (int) Math.Floor(Production * delta.TotalSeconds);
        }
        
        public void Tap() => TotalCookies += (int) CookiePerTap;
        
        public Building FindBuilding(string name) => buildings.Find(b => b.Name == name);
    }
}