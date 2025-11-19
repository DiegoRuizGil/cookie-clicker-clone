using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Effects;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Builders
{
    public class UpgradeBuilder : IBuilder<Upgrade>
    {
        private string _name;
        private Sprite _icon;
        private int _cost;
        private string _description;
        private IUpgradeEffect _effect;

        public UpgradeBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UpgradeBuilder WithIcon(Sprite icon)
        {
            _icon = icon;
            return this;
        }

        public UpgradeBuilder WithCost(int cost)
        {
            _cost = cost;
            return this;
        }

        public UpgradeBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
        
        public UpgradeBuilder WithEffect(IUpgradeEffect effect)
        {
            _effect = effect;
            return this;
        }
        
        public Upgrade Build()
        {
            Assert.IsNotNull(_effect);
            
            return new Upgrade(_name, _icon, _cost, _description, _effect);
        }
    }
}