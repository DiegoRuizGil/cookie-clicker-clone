using Cookie_Clicker.Runtime.Cookies.Domain;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class BuildingTests
    {
        [Test]
        public void EmptyCase()
        {
            var building = new Building(1);
            
            Assert.That(building.Production, Is.EqualTo(0));
        }

        [Test]
        public void BoostProduction_ByIncreasingBuildingQuantity()
        {
            var cps = 1;
            var building = new Building(cps);

            building.Quantity++;
            
            Assert.That(building.Production, Is.EqualTo(cps));
        }
    }
}