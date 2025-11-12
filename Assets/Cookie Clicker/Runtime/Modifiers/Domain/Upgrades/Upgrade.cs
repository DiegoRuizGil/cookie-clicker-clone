using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;

namespace Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades
{
    public abstract class Upgrade
    {
        private readonly List<IUnlockCondition> _unlockConditions = new List<IUnlockCondition>();
        protected bool IsUnlocked;
        
        public abstract void Apply(CookieBaker baker);

        public void AddUnlockCondition(IUnlockCondition condition) => _unlockConditions.Add(condition);
        public void AddUnlockConditions(IList<IUnlockCondition> conditions) => _unlockConditions.AddRange(conditions);
        public bool CanUnlock(CookieBaker baker) => _unlockConditions.All(condition => condition.IsMet(baker)) && !IsUnlocked;
        public void Unlock() => IsUnlocked = true;
    }
}