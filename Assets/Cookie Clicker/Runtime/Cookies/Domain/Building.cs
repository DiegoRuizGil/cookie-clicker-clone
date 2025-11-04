using System;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class Building
    {
        public string Name { get; }
        public int Production => _baseCPS * _quantity;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = Math.Max(value, 0);
        }

        private readonly int _baseCPS;
        private int _quantity;
        
        public Building(string name, int baseCPS)
        {
            Assert.IsTrue(baseCPS >= 0);

            Name = name;
            _baseCPS = baseCPS;
            _quantity = 1;
        }
    }
}