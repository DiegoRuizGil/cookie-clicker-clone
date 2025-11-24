using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain.Buildings
{
    public class Building
    {
        public double Production => cps.Value * Amount;
        public int Amount
        {
            get => _amount;
            set => _amount = Math.Max(value, 0);
        }
        
        public readonly string name;
        public readonly double baseCost;
        public readonly ProductionStat cps;
        public readonly Sprite icon;
        public readonly Sprite iconSilhouette;
        public double TotalBaked { get; private set; }

        private int _amount;
        private const float CostIncrease = 1.15f;
        private const float RefoundRate = 0.25f;

        public Building(string name, double baseCPS, double baseCost, Sprite icon, Sprite iconSilhouette)
        {
            Assert.IsTrue(baseCPS >= 0);

            this.name = name;
            cps = new ProductionStat(baseCPS);
            this.icon = icon;
            this.iconSilhouette = iconSilhouette;
            this.baseCost = baseCost;
            _amount = 0;
        }

        public double Bake(TimeSpan deltaTime)
        {
            var cookies = Production * deltaTime.TotalSeconds;
            TotalBaked += cookies;
            return cookies;
        }

        public double CostOf(int amountToBuy)
        {
            return baseCost * (MathF.Pow(CostIncrease, amountToBuy) - 1) / (CostIncrease - 1) * MathF.Pow(CostIncrease, _amount);
        }

        public double RefoundOf(int amountToSell)
        {
            var clampAmountToSell = Math.Clamp(amountToSell, 0, _amount);
            return baseCost * RefoundRate * (MathF.Pow(CostIncrease, clampAmountToSell) - 1) / (CostIncrease - 1) * MathF.Pow(CostIncrease, clampAmountToSell);
        }
    }
}