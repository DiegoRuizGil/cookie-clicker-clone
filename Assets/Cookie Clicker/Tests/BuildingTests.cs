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
    }
}