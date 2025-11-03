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
            var building = new Building(10);

            building.Quantity++;
            baker.buildings.Add(building);
            
            Assert.That(baker.Production, Is.EqualTo(10));
        }
    }
}