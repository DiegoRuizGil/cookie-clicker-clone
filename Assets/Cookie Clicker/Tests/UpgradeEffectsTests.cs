using Cookie_Clicker.Runtime.Builders;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;
using Cookie_Clicker.Runtime.Modifiers.Domain.Effects;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class UpgradeEffectsTests
    {
        [Test]
        public void ApplyBuildingEfficiencyUpgrade()
        {
            var upgrade = new EfficiencyEffect("cursor", 2.0f);
            var building = A.Building.WithName("cursor").WithBaseCPS(10).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { building });
            baker.AddBuilding(building.name);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.Production, Is.EqualTo(20));
        }

        [Test]
        public void ApplyGlobalProductionUpgrade()
        {
            var upgrade = new CookiesEffect(Percentage.FromPercentage(1f));
            var building = A.Building.WithName("cursor").WithBaseCPS(100).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { building });
            baker.AddBuilding(building.name);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.Production, Is.EqualTo(101));
        }

        [Test]
        public void ApplyTappingCursorUpgrade()
        {
            var upgrade = new TappingCursorEffect("cursor", 2.0f);
            var building = A.Building.WithName("cursor").WithBaseCPS(10).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { building });
            baker.AddBuilding(building.name);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.Production, Is.EqualTo(20));
            Assert.That(baker.CookiePerTap, Is.EqualTo(2));
        }

        [Test]
        public void TryToApplyGrandmaUpgrade_OnlyOneGrandma_NoUpgradeApplied()
        {
            var upgrade = new GrandmaEffect("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var grandma = A.Building.WithName("grandma").WithBaseCPS(10).Build();
            var farm = A.Building.WithName("farm").WithBaseCPS(100).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { grandma, farm });
            baker.AddBuilding(grandma.name);
            baker.AddBuilding(farm.name);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(10));
            Assert.That(farm.Production, Is.EqualTo(100));
        }

        [Test]
        public void ApplyGrandmaUpgrade_TwoGrandmas_UpgradeApplied()
        {
            var upgrade = new GrandmaEffect("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var grandma = A.Building.WithName("grandma").WithBaseCPS(10).Build();
            var farm = A.Building.WithName("farm").WithBaseCPS(100).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { grandma, farm });
            baker.AddBuilding(grandma.name, 2);
            baker.AddBuilding(farm.name);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(40));
            Assert.That(farm.Production, Is.EqualTo(101));
        }

        [Test]
        public void ApplyGrandmaUpgrade_SixGrandmas_UpgradeBuildingThreeTimes()
        {
            var upgrade = new GrandmaEffect("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var grandma = A.Building.WithName("grandma").WithBaseCPS(10).Build();
            var farm = A.Building.WithName("farm").WithBaseCPS(100).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { grandma, farm });
            baker.AddBuilding(grandma.name, 6);
            baker.AddBuilding(farm.name);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(120));
            Assert.That(farm.Production, Is.EqualTo(103));
        }

        [Test]
        public void ApplyGrandmaUpgrade_SevenGrandmas_UpgradeBuildingThreeTimes()
        {
            var upgrade = new GrandmaEffect("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var grandma = A.Building.WithName("grandma").WithBaseCPS(10).Build();
            var farm = A.Building.WithName("farm").WithBaseCPS(100).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { grandma, farm });
            baker.AddBuilding(grandma.name, 7);
            baker.AddBuilding(farm.name);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(140));
            Assert.That(farm.Production, Is.EqualTo(103));
        }

        [Test]
        public void UpdateUpgrade_WhenAddingMoreGrandmas()
        {
            var upgrade = new GrandmaEffect("grandma", "farm", 2.0f, Percentage.FromPercentage(1), 2);
            var grandma = A.Building.WithName("grandma").WithBaseCPS(10).Build();
            var farm = A.Building.WithName("farm").WithBaseCPS(100).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { grandma, farm });
            baker.AddBuilding(grandma.name, 3);
            baker.AddBuilding(farm.name);
            
            upgrade.Apply(baker);
            baker.AddBuilding(grandma.name);
            
            upgrade.Apply(baker);
            
            Assert.That(grandma.Production, Is.EqualTo(80));
            Assert.That(farm.Production, Is.EqualTo(102));
        }

        [Test]
        public void ApplyTappingUpgrade()
        {
            var upgrade = new TappingEffect(Percentage.FromPercentage(1f));
            var building = A.Building.WithName("cursor").WithBaseCPS(100).Build();
            var baker = new CookieBaker();
            baker.SetBuildings(new [] { building });
            baker.AddBuilding(building.name);
            
            upgrade.Apply(baker);
            
            Assert.That(baker.CookiePerTap, Is.EqualTo(2f));
        }

        [Test]
        public void ApplyTappingUpgrade_WithZeroCPS()
        {
            var upgrade = new TappingEffect(Percentage.FromPercentage(1f));
            var baker = new CookieBaker();
            
            upgrade.Apply(baker);
            
            Assert.That(baker.CookiePerTap, Is.EqualTo(1f));
        }
    }
}