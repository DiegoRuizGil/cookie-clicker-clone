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
        private int _baseCost;
        private const float CostIncrease = 1.15f;

        public Building(string name, float baseCPS, int baseCost = 0)
        {
            Assert.IsTrue(baseCPS >= 0);

            this.name = name;
            _amount = 0;
            cps = new ProductionStat(baseCPS);
            _baseCost = baseCost;
        }

        public float CostOf(int amountToBuy)
        {
            return _baseCost * (MathF.Pow(CostIncrease, amountToBuy) - 1) / (CostIncrease - 1) * MathF.Pow(CostIncrease, _amount);
        }
    }
}