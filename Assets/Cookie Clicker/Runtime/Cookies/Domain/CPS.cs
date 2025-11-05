namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CPS
    {
        public float Value => _base * _efficiency + _multiplier.AppliedTo(_base * _efficiency);
        
        private readonly float _base;
        private float _efficiency;
        private Percentage _multiplier;
        
        public CPS(float baseCPS)
        {
            _base = baseCPS;
            _efficiency = 1.0f;
            _multiplier = Percentage.Zero();
        }
        
        public void AddEfficiency(float efficiency) => _efficiency *= efficiency;
        public void AddMultiplier(Percentage multiplier) => _multiplier += multiplier;
    }
}