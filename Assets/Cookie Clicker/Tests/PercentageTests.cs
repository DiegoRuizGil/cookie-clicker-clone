using Cookie_Clicker.Runtime.Cookies.Domain;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class PercentageTests
    {
        [Test]
        public void ApplyPercentage()
        {
            var percentage = Percentage.FromPercentage(25f);
            
            Assert.That(percentage.AppliedTo(100f), Is.EqualTo(25f));
        }
    }
}