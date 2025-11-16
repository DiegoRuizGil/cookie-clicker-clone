using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class BuildingProgressData
    {
        public enum Visibility
        {
            Hidden, NotRevealed, Revealed
        }
        
        public readonly Building building;
        public Visibility visibility;

        public BuildingProgressData(Building building)
        {
            this.building = building;
            visibility = Visibility.Hidden;
        }
    }
    
    public class BuildingsProgression
    {
        public readonly List<BuildingProgressData> buildings = new List<BuildingProgressData>();

        public BuildingsProgression(IList<Building> buildings)
        {
            Assert.IsTrue(buildings.Count >= 2);
            
            foreach (var building in buildings)
                this.buildings.Add(new BuildingProgressData(building));
            
            this.buildings[0].visibility = BuildingProgressData.Visibility.NotRevealed;
            this.buildings[1].visibility = BuildingProgressData.Visibility.NotRevealed;
        }

        public void Update(float totalCookies)
        {
            TryRevealNext(totalCookies);
            EnsureNotRevealedSlots();
        }

        private void TryRevealNext(float totalCookies)
        {
            var candidate = buildings
                .Where(b => b.visibility == BuildingProgressData.Visibility.NotRevealed)
                .FirstOrDefault(b => b.building.baseCost <= totalCookies);
            
            if (candidate != null)
                candidate.visibility = BuildingProgressData.Visibility.Revealed;
        }

        private void EnsureNotRevealedSlots()
        {
            int notRevealedCount = buildings.Count(b => b.visibility == BuildingProgressData.Visibility.NotRevealed);

            while (notRevealedCount < 2)
            {
                var nextHidden = buildings.FirstOrDefault(b => b.visibility == BuildingProgressData.Visibility.Hidden);
                if (nextHidden == null)
                    break;
                
                nextHidden.visibility = BuildingProgressData.Visibility.NotRevealed;
                notRevealedCount++;
            }
        }
    }
}