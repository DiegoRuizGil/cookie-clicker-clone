namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CPS
    {
        public float Value => _base * _efficiency * _multiplier;
        
        private readonly float _base;
        private float _efficiency;
        private float _multiplier;
        
        public CPS(float baseCPS)
        {
            _base = baseCPS;
            _efficiency = 1.0f;
            _multiplier = 1.0f;
        }
        
        public void AddEfficiency(float efficiency) => _efficiency *= efficiency;
        public void AddMultiplier(float multiplier) => _multiplier += multiplier;
    }
}