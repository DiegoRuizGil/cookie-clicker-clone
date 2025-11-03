using System;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class Building
    {
        public int Production => _baseCPS * _quantity;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = Math.Max(value, 0);
        }

        private readonly int _baseCPS;
        private int _quantity;
        
        public Building(int baseCPS)
        {
            _baseCPS = baseCPS;
            _quantity = 1;
        }
    }
}