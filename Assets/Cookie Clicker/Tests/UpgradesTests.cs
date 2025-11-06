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
            var upgrade = new EfficiencyUpgrade("cursor", 2.0f);
            var baker = new CookieBaker();
            var building = new Building("cursor", 10);
            baker.AddBuilding(building);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.Production, Is.EqualTo(20));
        }

        [Test]
        public void ApplyGlobalProductionUpgrade()
        {
            var upgrade = new GlobalProductionUpgrade(Percentage.FromPercentage(1f));
            var baker = new CookieBaker();
            var building = new Building("cursor", 100);
            baker.AddBuilding(building);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.Production, Is.EqualTo(101));
        }

        [Test]
        public void ApplyTappingCursorUpgrade()
        {
            var upgrade = new TappingCursorUpgrade("cursor", 2.0f);
            var baker = new CookieBaker();
            var building = new Building("cursor", 10);
            baker.AddBuilding(building);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.Production, Is.EqualTo(20));
            Assert.That(baker.CookiePerTap, Is.EqualTo(2));
        }

        [Test]
        public void TryToApplyGrandmaUpgrade_OnlyOneGrandma_NoUpgradeApplied()
        {
            var upgrade = new GrandmaUpgrade("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var baker = new CookieBaker();
            var grandma = new Building("grandma", 10);
            var farm = new Building("farm", 100);
            baker.AddBuilding(grandma);
            baker.AddBuilding(farm);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(10));
            Assert.That(farm.Production, Is.EqualTo(100));
        }

        [Test]
        public void ApplyGrandmaUpgrade_TwoGrandmas_UpgradeApplied()
        {
            var upgrade = new GrandmaUpgrade("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var baker = new CookieBaker();
            var grandma = new Building("grandma", 10);
            var farm = new Building("farm", 100);
            baker.AddBuilding(grandma, 2);
            baker.AddBuilding(farm);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(40));
            Assert.That(farm.Production, Is.EqualTo(101));
        }

        [Test]
        public void ApplyGrandmaUpgrade_SixGrandmas_UpgradeBuildingThreeTimes()
        {
            var upgrade = new GrandmaUpgrade("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var baker = new CookieBaker();
            var grandma = new Building("grandma", 10);
            var farm = new Building("farm", 100);
            baker.AddBuilding(grandma, 6);
            baker.AddBuilding(farm);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(120));
            Assert.That(farm.Production, Is.EqualTo(103));
        }

        [Test]
        public void ApplyGrandmaUpgrade_SevenGrandmas_UpgradeBuildingThreeTimes()
        {
            var upgrade = new GrandmaUpgrade("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var baker = new CookieBaker();
            var grandma = new Building("grandma", 10);
            var farm = new Building("farm", 100);
            baker.AddBuilding(grandma, 7);
            baker.AddBuilding(farm);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(140));
            Assert.That(farm.Production, Is.EqualTo(103));
        }

        [Test]
        public void UpdateUpgrade_WhenAddingMoreGrandmas()
        {
            var upgrade = new GrandmaUpgrade("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var baker = new CookieBaker();
            var grandma = new Building("grandma", 10);
            var farm = new Building("farm", 100);
            baker.AddBuilding(grandma, 3);
            baker.AddBuilding(farm);
            
            upgrade.Apply(baker);
            baker.AddBuilding(grandma);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(80));
            Assert.That(farm.Production, Is.EqualTo(102));
        }

        [Test]
        public void ApplyTappingUpgrade()
        {
            var upgrade = new TappingUpgrade(Percentage.FromPercentage(1f));
            var baker = new CookieBaker();
            var building = new Building("cursor", 100);
            baker.AddBuilding(building);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.CookiePerTap, Is.EqualTo(2f));
        }

        [Test]
        public void ApplyTappingUpgrade_WithZeroCPS()
        {
            var upgrade = new TappingUpgrade(Percentage.FromPercentage(1f));
            var baker = new CookieBaker();
            
            upgrade.Apply(baker);
            
            Assert.That(baker.CookiePerTap, Is.EqualTo(1f));
        }
    }
}