using System;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class Building
    {
        public float Production => cps.Value * Amount;
        public int Amount
        {
            get => _amount;
            set => _amount = Math.Max(value, 0);
        }
        
        public readonly string name;
        public readonly ProductionStat cps;

        private int _amount;


        public Building(string name, float baseCPS)
        {
            Assert.IsTrue(baseCPS >= 0);

            this.name = name;
            _amount = 0;
            cps = new ProductionStat(baseCPS);
        }
    }
}