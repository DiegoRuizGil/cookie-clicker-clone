using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    public abstract class BaseUpgradeConfig : ScriptableObject
    {
        protected Upgrade Upgrade;
        
        public abstract void Init();
        
        public void Apply(CookieBaker baker) => Upgrade.Apply(baker);
    }
}