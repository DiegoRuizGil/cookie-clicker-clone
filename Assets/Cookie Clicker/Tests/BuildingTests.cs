using Cookie_Clicker.Runtime.Builders;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class BuildingTests
    {
        [Test]
        public void EmptyCase()
        {
            var building = A.Building.WithName("Cursor").WithBaseCPS(1).Build();
            
            Assert.That(building.Production, Is.EqualTo(0));
        }

        [Test]
        public void BoostProduction_ByIncreasingBuildingQuantity()
        {
            var cps = 2;
            var building = A.Building.WithName("Cursor").WithBaseCPS(cps).Build();

            building.Amount += 2;
            
            Assert.That(building.Production, Is.EqualTo(cps * 2));
        }
    }
}