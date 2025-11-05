using System;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public struct Percentage : IEquatable<Percentage>
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
        public static Percentage Zero() => new Percentage(0f);
        
        public float AppliedTo(float baseValue) => baseValue * _value;

        public static Percentage operator +(Percentage a, Percentage b) => new Percentage(a._value + b._value);
        public static Percentage operator -(Percentage a, Percentage b) => new Percentage(a._value - b._value);
        public static Percentage operator *(Percentage a, float value) => new Percentage(a._value * value);
        public static Percentage operator *(float value, Percentage a) => new Percentage(a._value * value);
        public static Percentage operator /(Percentage a, float value) => new Percentage(a._value / value);
        public static Percentage operator /(float value, Percentage a) => new Percentage(value / a._value);
        
        public static implicit operator float(Percentage percent) => percent._value;

        public bool Equals(Percentage other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is Percentage other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => $"{AsPercentage:0.##}%";
    }
}