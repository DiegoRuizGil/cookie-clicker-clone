using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    public abstract class BaseUpgradeConfig : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField] protected string upgradeName;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected int cost;
        
        public abstract Upgrade Get();
    }
}