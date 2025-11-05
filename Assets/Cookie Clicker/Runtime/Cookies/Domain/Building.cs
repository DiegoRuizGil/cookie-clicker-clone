using System;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class Building
    {
        public float Production => cps.Value * Quantity;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = Math.Max(value, 0);
        }
        
        public readonly string name;
        public readonly ProductionStat cps;

        private int _quantity;


        public Building(string name, float baseCPS)
        {
            Assert.IsTrue(baseCPS >= 0);

            this.name = name;
            _quantity = 0;
            cps = new ProductionStat(baseCPS);
        }
    }
}