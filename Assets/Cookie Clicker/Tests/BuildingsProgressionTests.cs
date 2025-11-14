using System.Collections.Generic;
using Cookie_Clicker.Runtime.Builders;
using Cookie_Clicker.Runtime.Cookies.Domain;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class BuildingsProgressionTests
    {
        [Test]
        public void EmptyCase()
        {
            var buildings = CreateBuildings(5);
            var progression = new BuildingsProgression(buildings);
            
            Assert.That(BuildingRevealState.NotRevealed, Is.EqualTo(progression.buildings[0].State));
            Assert.That(BuildingRevealState.NotRevealed, Is.EqualTo(progression.buildings[1].State));
            
            for(int i = 2; i < 5; i++)
                Assert.That(BuildingRevealState.Hidden, Is.EqualTo(progression.buildings[i].State));
        }

        private List<Building> CreateBuildings(int amount)
        {
            var list = new List<Building>();
            for (int i = 0; i < amount; i++)
                list.Add(A.Building.WithName($"building{i}").Build());
            
            return list;
        }
    }
}