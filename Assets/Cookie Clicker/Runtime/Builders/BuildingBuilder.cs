using Cookie_Clicker.Runtime.Cookies.Domain.Buildings;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Builders
{
    public class BuildingBuilder : IBuilder<Building>
    {
        private string _name;
        private double _baseCPS;
        private double _baseCost;
        private Sprite _icon;
        private Sprite _iconSilhouette;

        public BuildingBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public BuildingBuilder WithBaseCPS(double baseCPS)
        {
            _baseCPS = baseCPS;
            return this;
        }

        public BuildingBuilder WithBaseCost(double baseCost)
        {
            _baseCost = baseCost;
            return this;
        }

        public BuildingBuilder WithIcon(Sprite icon)
        {
            _icon = icon;
            return this;
        }

        public BuildingBuilder WithIconSilhouette(Sprite silhouette)
        {
            _iconSilhouette = silhouette;
            return this;
        }
        
        public Building Build()
        {
            var building = new Building(_name, _baseCPS, _baseCost, _icon, _iconSilhouette);
            return building;
        }

        public static implicit operator Building(BuildingBuilder builder)
        {
            return builder.Build();
        }
    }
}