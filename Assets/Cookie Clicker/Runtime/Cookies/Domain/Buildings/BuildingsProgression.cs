using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain.Buildings
{
    public enum BuildingVisibility
    {
        Hidden, Locked, Unlocked
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
        public readonly List<BuildingProgressData> buildingsData = new List<BuildingProgressData>();

        public event Action<BuildingProgressData> OnBuildingVisibilityChanged = delegate { };
        
        public BuildingsProgression(IList<Building> buildings)
        {
            Assert.IsTrue(buildings.Count >= 2);
            
            foreach (var building in buildings)
                buildingsData.Add(new BuildingProgressData(building));
        }

        public void Init()
        {
            EnsureNotRevealedSlots();
        }

        public void Update(double totalCookies)
        {
            TryRevealNext(totalCookies);
            EnsureNotRevealedSlots();
        }

        public BuildingVisibility GetVisibility(string buildingName)
        {
            return buildingsData.Find(data => data.building.name == buildingName).visibility;
        }

        private void TryRevealNext(double totalCookies)
        {
            var candidate = buildingsData
                .Where(b => b.visibility == BuildingVisibility.Locked)
                .FirstOrDefault(b => b.building.baseCost <= totalCookies);

            if (candidate != null)
            {
                candidate.visibility = BuildingVisibility.Unlocked;
                OnBuildingVisibilityChanged.Invoke(candidate);
            }
        }

        private void EnsureNotRevealedSlots()
        {
            int notRevealedCount = buildingsData.Count(b => b.visibility == BuildingVisibility.Locked);

            while (notRevealedCount < 2)
            {
                var nextHidden = buildingsData.FirstOrDefault(b => b.visibility == BuildingVisibility.Hidden);
                if (nextHidden == null)
                    break;
                
                nextHidden.visibility = BuildingVisibility.Locked;
                OnBuildingVisibilityChanged.Invoke(nextHidden);
                notRevealedCount++;
            }
        }
    }
}