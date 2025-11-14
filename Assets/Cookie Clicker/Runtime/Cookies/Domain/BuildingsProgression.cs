using System.Collections.Generic;
using NUnit.Framework;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public enum BuildingRevealState
    {
        Hidden, NotRevealed, Revealed
    }
    
    public class BuildingProgressInfo
    {
        public Building Building { get; }
        public BuildingRevealState State { get; set; }

        public BuildingProgressInfo(Building building)
        {
            Building = building;
            State = BuildingRevealState.Hidden;
        }
    }
    
    public class BuildingsProgression
    {
        public readonly List<BuildingProgressInfo> buildings = new List<BuildingProgressInfo>();

        public BuildingsProgression(IList<Building> buildings)
        {
            Assert.IsTrue(buildings.Count >= 2);
            
            foreach (var building in buildings)
                this.buildings.Add(new BuildingProgressInfo(building));
            
            this.buildings[0].State = BuildingRevealState.NotRevealed;
            this.buildings[1].State = BuildingRevealState.NotRevealed;
        }
    }
}