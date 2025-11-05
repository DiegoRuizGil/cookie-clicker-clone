using System;
using System.Collections.Generic;
using System.Linq;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CookieBaker
    {
        public readonly List<Building> buildings = new List<Building>();
        public float Production => buildings.Sum(b => b.Production) * productionMultiplier;
        public float CookiePerTap => BaseCookiesPerTap * tapMultiplier;
        public int TotalCookies { get; private set; }

        public float productionMultiplier = 1f;
        public float tapMultiplier = 1f;

        private const int BaseCookiesPerTap = 1;

        public void PassTime(TimeSpan delta)
        {
            TotalCookies += (int) Math.Floor(Production * delta.TotalSeconds);
        }
        
        public void Tap() => TotalCookies += (int) CookiePerTap;
        
        public Building FindBuilding(string name) => buildings.Find(b => b.Name == name);
    }
}