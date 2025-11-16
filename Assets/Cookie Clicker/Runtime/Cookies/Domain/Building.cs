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
        public readonly int baseCost;
        public readonly ProductionStat cps;

        private int _amount;
        private const float CostIncrease = 1.15f;
        private const float RefoundRate = 0.25f;

        public Building(string name, float baseCPS, int baseCost)
        {
            Assert.IsTrue(baseCPS >= 0);

            this.name = name;
            _amount = 0;
            cps = new ProductionStat(baseCPS);
            this.baseCost = baseCost;
        }

        public float CostOf(int amountToBuy)
        {
            return baseCost * (MathF.Pow(CostIncrease, amountToBuy) - 1) / (CostIncrease - 1) * MathF.Pow(CostIncrease, _amount);
        }

        public float RefoundOf(int amountToSell)
        {
            var clampAmountToSell = Math.Clamp(amountToSell, 0, _amount);
            return baseCost * RefoundRate * (MathF.Pow(CostIncrease, clampAmountToSell) - 1) / (CostIncrease - 1) * MathF.Pow(CostIncrease, clampAmountToSell);
        }
    }
}