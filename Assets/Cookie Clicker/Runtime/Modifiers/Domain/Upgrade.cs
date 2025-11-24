using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Baker;
using Cookie_Clicker.Runtime.Modifiers.Domain.Effects;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class Upgrade
    {
        public readonly string name;
        public readonly Sprite icon;
        public readonly double cost;
        public readonly string description;
        
        public bool IsUnlocked { get; private set; }
        
        private readonly List<IUnlockCondition> _unlockConditions = new List<IUnlockCondition>();
        private readonly IUpgradeEffect _effect;
        
        public Upgrade(string name, Sprite icon, double cost, string description, IUpgradeEffect effect)
        {
            this.name = name;
            this.icon = icon;
            this.cost = cost;
            this.description = description;
            _effect = effect;
        }

        public void Apply(CookieBaker baker) => _effect.Apply(baker);

        public void AddUnlockCondition(IUnlockCondition condition) => _unlockConditions.Add(condition);
        public void AddUnlockConditions(IList<IUnlockCondition> conditions) => _unlockConditions.AddRange(conditions);
        public bool CanUnlock(CookieBaker baker) => _unlockConditions.All(condition => condition.IsMet(baker)) && !IsUnlocked;
        public void Unlock() => IsUnlocked = true;
    }
}