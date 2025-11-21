using Cookie_Clicker.Runtime.Cookies.Domain;
using NUnit.Framework;

namespace Cookie_Clicker.Tests
{
    public class StringUtilsTests
    {
        [Test]
        public void FormatNumberSmallerThanMillion()
        {
            var formatNumber = StringUtils.FormatNumber(234546.12f);
            Assert.That(formatNumber, Is.EqualTo("234.546,1"));
        }

        [Test]
        public void FormatMillion()
        {
            var formatNumber = StringUtils.FormatNumber(12_000_000.35);
            Assert.That(formatNumber, Is.EqualTo("12,000 million"));
        }
        
        [Test]
        public void FormatBillion()
        {
            var formatNumber = StringUtils.FormatNumber(12_000_000_000.35);
            Assert.That(formatNumber, Is.EqualTo("12,000 billion"));
        }
    }
}