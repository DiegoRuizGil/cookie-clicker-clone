using System.Collections.Generic;
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
    }
}