using System;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class Building
    {
        public string Name { get; }
        public float Production => _baseCPS * _quantity * cpsMultiplier;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = Math.Max(value, 0);
        }
        public float cpsMultiplier = 1.0f;

        private readonly float _baseCPS;
        private int _quantity;
        
        public Building(string name, float baseCPS)
        {
            Assert.IsTrue(baseCPS >= 0);

            Name = name;
            _baseCPS = baseCPS;
            _quantity = 1;
        }
    }
}