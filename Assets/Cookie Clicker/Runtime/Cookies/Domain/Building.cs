using System;
using UnityEngine;
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
        public readonly Sprite icon;
        public readonly string description;
        public float TotalBaked { get; private set; }

        private int _amount;
        private const float CostIncrease = 1.15f;
        private const float RefoundRate = 0.25f;

        public Building(string name, float baseCPS, int baseCost, Sprite icon, string description)
        {
            Assert.IsTrue(baseCPS >= 0);

            this.name = name;
            cps = new ProductionStat(baseCPS);
            this.icon = icon;
            this.description = description;
            this.baseCost = baseCost;
            _amount = 0;
        }

        public float Bake(TimeSpan deltaTime)
        {
            var cookies = Production * (float)deltaTime.TotalSeconds;
            TotalBaked += cookies;
            return cookies;
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