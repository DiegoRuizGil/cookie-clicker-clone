using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CookieBaker
    {
        public readonly List<Building> buildings = new List<Building>();
        public int Production => buildings.Sum(b => b.Production);
        public int TotalCookies { get; private set; }

        private readonly int _cookiesPerTap;
        
        public CookieBaker(int cookiesPerTap)
        {
            Assert.IsTrue(cookiesPerTap >= 0);
            
            _cookiesPerTap = cookiesPerTap;
        }
        
        public void PassTime(TimeSpan delta)
        {
            TotalCookies += (int)(Production * delta.TotalSeconds);
        }
        
        public void Tap() => TotalCookies += _cookiesPerTap;
    }
}