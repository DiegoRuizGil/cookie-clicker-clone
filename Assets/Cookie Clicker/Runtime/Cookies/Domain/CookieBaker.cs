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
        public float TotalCookies => HandMadeCookies + BakedCookies;
        public float HandMadeCookies { get; private set; }
        public float BakedCookies { get; private set; }
        public float CurrentCookies
        {
            get => _currentCookies;
            set => _currentCookies = Math.Max(value, 0);
        }

        public event Action<Building, int> OnBuildingAmountChanged = delegate { };
        
        public Percentage ProductionMultiplier { get; set; } = Percentage.Zero();
        public readonly ProductionStat tapping = new ProductionStat(1);
        
        private readonly Dictionary<string, Building> buildings = new  Dictionary<string, Building>();
        
        private float _currentCookies;
        
        public void Bake(TimeSpan deltaTime)
        {
            var cookies = buildings.Values.ToList().Sum(b => b.Bake(deltaTime));
            BakedCookies += cookies;
            CurrentCookies += cookies;
        }

        public void Tap()
        {
            HandMadeCookies += CookiePerTap;
            CurrentCookies += CookiePerTap;
        }

        public Building FindBuilding(string name) => buildings.GetValueOrDefault(name);
        public int OwnedBuildingsOf(string name) => buildings.TryGetValue(name, out var building) ? building.Amount : 0;
        public List<Building> GetBuildings() => buildings.Values.ToList();
        
        public void AddBuilding(string buildingName, int amount = 1)
        {
            Assert.IsTrue(amount > 0);

            TryModifyBuildingAmount(buildingName, amount);
        }

        public void RemoveBuilding(string buildingName, int amount = 1)
        {
            Assert.IsTrue(amount > 0);

            TryModifyBuildingAmount(buildingName, -amount);
        }

        private void TryModifyBuildingAmount(string buildingName, int amount)
        {
            if (buildings.TryGetValue(buildingName, out var building))
            {
                var previousAmount = building.Amount;
                building.Amount += amount;
                var delta = building.Amount - previousAmount;
                OnBuildingAmountChanged.Invoke(building, delta);
            }
        }

        public void SetBuildings(IList<Building> initialBuildings)
        {
            foreach (var building in initialBuildings)
                buildings[building.name] = building;
        }
    }
}