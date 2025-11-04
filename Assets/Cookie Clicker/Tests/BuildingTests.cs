using Cookie_Clicker.Runtime.Cookies.Domain;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class BuildingTests
    {
        [Test]
        public void EmptyCase()
        {
            var building = new Building("cursor", 1);
            
            Assert.That(building.Production, Is.EqualTo(1));
        }

        [Test]
        public void BoostProduction_ByIncreasingBuildingQuantity()
        {
            var cps = 2;
            var building = new Building("cursor", cps);

            building.Quantity++;
            
            Assert.That(building.Production, Is.EqualTo(cps * 2));
        }
    }
}