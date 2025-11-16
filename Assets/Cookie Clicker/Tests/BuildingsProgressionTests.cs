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
            
            Assert.That(BuildingProgressData.Visibility.NotRevealed, Is.EqualTo(progression.buildings[0].visibility));
            Assert.That(BuildingProgressData.Visibility.NotRevealed, Is.EqualTo(progression.buildings[1].visibility));
            
            for(int i = 2; i < 5; i++)
                Assert.That(BuildingProgressData.Visibility.Hidden, Is.EqualTo(progression.buildings[i].visibility));
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