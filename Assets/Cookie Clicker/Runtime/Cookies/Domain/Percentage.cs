namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public struct Percentage
    {
        private readonly float _value;

        public float Normalized => _value;
        public float AsPercentage => _value * 100f;

        private Percentage(float normalizedValue)
        {
            _value = normalizedValue;
        }
        
        public static Percentage FromFraction(float fraction) => new Percentage(fraction);
        public static Percentage FromPercentage(float percentage) => new Percentage(percentage / 100f);
        
        public float AppliedTo(float baseValue) => baseValue * _value;

        public static Percentage operator +(Percentage a, Percentage b) => new Percentage(a._value + b._value);
        public static Percentage operator -(Percentage a, Percentage b) => new Percentage(a._value - b._value);
        public static Percentage operator *(Percentage a, float scalar) => new Percentage(a._value * scalar);
        public static Percentage operator /(Percentage a, float scalar) => new Percentage(a._value / scalar);
        
        public override string ToString() => $"{AsPercentage:0.##}%";
    }
}