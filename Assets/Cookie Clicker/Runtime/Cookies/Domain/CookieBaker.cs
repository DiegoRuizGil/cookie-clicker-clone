using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CookieBaker
    {
        public float Production => buildings.Values.Sum(b => b.Production) * (1 + ProductionMultiplier);
        public float CookiePerTap => tapping.Base * tapping.Efficiency + (Production * tapping.Multiplier);
        public float TotalCookies { get; private set; }

        public Percentage ProductionMultiplier { get; set; } = Percentage.Zero();
        public readonly ProductionStat tapping = new ProductionStat(1);
        
        private readonly Dictionary<string, Building> buildings = new  Dictionary<string, Building>();

        public void PassTime(TimeSpan delta)
        {
            TotalCookies += Production * (float) delta.TotalSeconds;
        }
        
        public void Tap() => TotalCookies += (int) CookiePerTap;
        
        public Building FindBuilding(string name) => buildings.GetValueOrDefault(name);

        public void AddBuilding(Building buildingToAdd, int amount = 1)
        {
            Assert.IsTrue(amount > 0);
            
            buildings.TryAdd(buildingToAdd.name, buildingToAdd);
            buildingToAdd.Amount += amount;
        }
    }
}