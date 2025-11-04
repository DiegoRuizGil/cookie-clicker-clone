using System;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class Building
    {
        public string Name { get; }
        public float Production => cps.Value * Quantity;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = Math.Max(value, 0);
        }
        
        public readonly CPS cps;

        private int _quantity;


        public Building(string name, float baseCPS)
        {
            Assert.IsTrue(baseCPS >= 0);

            Name = name;
            _quantity = 1;
            cps = new CPS(baseCPS);
        }
    }
}