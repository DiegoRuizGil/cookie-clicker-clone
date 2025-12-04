using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    public abstract class BaseUpgradeConfig : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField] protected string upgradeName;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected double cost;
        [SerializeField, TextArea] protected string description;
        
        public string Name => upgradeName;
        
        public abstract Upgrade Get();
    }
}