namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class ProductionStat
    {
        public float Value => Base * Efficiency * (1 + Multiplier);
        
        public float Base { get; }
        public float Efficiency { get; private set; }
        public Percentage Multiplier { get; private set; }
        
        public ProductionStat(float baseCPS)
        {
            Base = baseCPS;
            Efficiency = 1.0f;
            Multiplier = Percentage.Zero();
        }
        
        public void AddEfficiency(float efficiency) => Efficiency *= efficiency;
        public void AddMultiplier(Percentage multiplier) => Multiplier += multiplier;
    }
}