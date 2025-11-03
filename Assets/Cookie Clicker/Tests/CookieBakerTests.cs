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
    }
}