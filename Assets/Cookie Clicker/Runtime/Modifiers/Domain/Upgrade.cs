using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public abstract class Upgrade
    {
        private readonly List<IUnlockCondition> _unlockConditions = new List<IUnlockCondition>();
        protected bool IsUnlocked;
        
        public abstract void Apply(CookieBaker baker);

        public void AddUnlockCondition(IUnlockCondition condition) => _unlockConditions.Add(condition);
        public bool CanUnlock(CookieBaker baker) => _unlockConditions.All(condition => condition.IsMet(baker));
        public void Unlock() => IsUnlocked = true;
    }
}