using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Builders
{
    public class BuildingBuilder : IBuilder<Building>
    {
        private string _name;
        private float _baseCPS;
        private int _baseCost;

        public BuildingBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public BuildingBuilder WithBaseCPS(float baseCPS)
        {
            _baseCPS = baseCPS;
            return this;
        }

        public BuildingBuilder WithBaseCost(int baseCost)
        {
            _baseCost = baseCost;
            return this;
        }
        
        public Building Build()
        {
            var building = new Building(_name, _baseCPS, _baseCost);
            return building;
        }

        public static implicit operator Building(BuildingBuilder builder)
        {
            return builder.Build();
        }
    }
}