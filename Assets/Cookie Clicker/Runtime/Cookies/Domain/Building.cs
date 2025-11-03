namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class Building
    {
        public int Production => _baseCPS * _quantity;

        private readonly int _baseCPS;
        private int _quantity;
        
        public Building(int baseCPS)
        {
            _baseCPS = baseCPS;
        }
    }
}