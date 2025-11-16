using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public enum BuildingVisibility
    {
        Hidden, NotRevealed, Revealed
    }

    public class BuildingProgressData
    {
        public readonly Building building;
        public BuildingVisibility visibility;

        public BuildingProgressData(Building building)
        {
            this.building = building;
            visibility = BuildingVisibility.Hidden;
        }
    }

    public class BuildingsProgression
    {
        public readonly List<BuildingProgressData> buildings = new List<BuildingProgressData>();

        public event Action<BuildingProgressData> OnBuildingVisibilityChanged = delegate { };
        
        public BuildingsProgression(IList<Building> buildings)
        {
            Assert.IsTrue(buildings.Count >= 2);
            
            foreach (var building in buildings)
                this.buildings.Add(new BuildingProgressData(building));
        }

        public void Init()
        {
            EnsureNotRevealedSlots();
        }

        public void Update(float totalCookies)
        {
            TryRevealNext(totalCookies);
            EnsureNotRevealedSlots();
        }

        private void TryRevealNext(float totalCookies)
        {
            var candidate = buildings
                .Where(b => b.visibility == BuildingVisibility.NotRevealed)
                .FirstOrDefault(b => b.building.baseCost <= totalCookies);

            if (candidate != null)
            {
                candidate.visibility = BuildingVisibility.Revealed;
                OnBuildingVisibilityChanged.Invoke(candidate);
            }
        }

        private void EnsureNotRevealedSlots()
        {
            int notRevealedCount = buildings.Count(b => b.visibility == BuildingVisibility.NotRevealed);

            while (notRevealedCount < 2)
            {
                var nextHidden = buildings.FirstOrDefault(b => b.visibility == BuildingVisibility.Hidden);
                if (nextHidden == null)
                    break;
                
                nextHidden.visibility = BuildingVisibility.NotRevealed;
                OnBuildingVisibilityChanged.Invoke(nextHidden);
                notRevealedCount++;
            }
        }
    }
}