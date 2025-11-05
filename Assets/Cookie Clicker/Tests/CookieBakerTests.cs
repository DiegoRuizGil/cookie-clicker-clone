using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class CookieBakerTests
    {
        [Test]
        public void EmptyCase()
        {
            var baker = new CookieBaker();

            baker.PassTime(TimeSpan.FromSeconds(1));
            
            Assert.That(baker.TotalCookies, Is.EqualTo(0));
        }

        [Test]
        public void AddBuilding_BoostProduction()
        {
            var baker = new CookieBaker();
            var building = new Building("cursor", 10);

            baker.buildings.Add(building);
            
            Assert.That(baker.Production, Is.EqualTo(10));
        }

        [Test]
        public void PassTime_ProduceCookies()
        {
            var baker = new CookieBaker();
            var building = new Building("cursor", 10);
            
            baker.buildings.Add(building);
            baker.PassTime(TimeSpan.FromSeconds(1));
            
            Assert.That(baker.TotalCookies, Is.EqualTo(10));
        }

        [Test]
        public void ProduceCookiesByTapping()
        {
            var baker = new CookieBaker();

            baker.Tap();
            
            Assert.That(baker.TotalCookies, Is.EqualTo(1));
        }
    }
}