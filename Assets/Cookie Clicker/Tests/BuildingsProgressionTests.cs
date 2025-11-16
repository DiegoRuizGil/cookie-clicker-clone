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

        [Test]
        public void RevealNextBuilding()
        {
            var buildings = CreateBuildingsWithCost(5, new [] { 100, 200, 300, 400, 500 });
            var progression = new BuildingsProgression(buildings);

            progression.Update(100);
            
            Assert.That(BuildingProgressData.Visibility.Revealed, Is.EqualTo(progression.buildings[0].visibility));
            Assert.That(BuildingProgressData.Visibility.NotRevealed, Is.EqualTo(progression.buildings[1].visibility));
            Assert.That(BuildingProgressData.Visibility.NotRevealed, Is.EqualTo(progression.buildings[2].visibility));
            
            for(int i = 3; i < 5; i++)
                Assert.That(BuildingProgressData.Visibility.Hidden, Is.EqualTo(progression.buildings[i].visibility));
        }

        private List<Building> CreateBuildings(int amount)
        {
            var list = new List<Building>();
            for (int i = 0; i < amount; i++)
                list.Add(A.Building.WithName($"building{i}").Build());
            
            return list;
        }
        
        private List<Building> CreateBuildingsWithCost(int amount, int[] costs)
        {
            var list = new List<Building>();
            for (int i = 0; i < amount; i++)
            {
                var building = A.Building.WithName($"building{i}").WithBaseCost(costs[i]).Build();
                list.Add(building);
            }
            
            return list;
        }
    }
}