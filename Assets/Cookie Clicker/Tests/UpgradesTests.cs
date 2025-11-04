using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class UpgradesTests
    {
        [Test]
        public void ApplyBuildingEfficiencyUpgrade()
        {
            var upgrade = new BuildingEfficiencyUpgrade("cursor", 2.0f);
            var baker = new CookieBaker(10);
            var building = new Building("cursor", 10);
            baker.buildings.Add(building);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.Production, Is.EqualTo(20));
        }
    }
}