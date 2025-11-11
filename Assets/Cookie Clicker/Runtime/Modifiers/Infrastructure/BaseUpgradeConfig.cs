using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    public abstract class BaseUpgradeConfig : ScriptableObject
    {
        public abstract Upgrade Get();
    }
}