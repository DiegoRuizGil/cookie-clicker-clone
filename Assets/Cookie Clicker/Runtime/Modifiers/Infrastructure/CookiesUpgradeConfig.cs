using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Global Production")]
    public class CookiesUpgradeConfig : BaseUpgradeConfig
    {
        [Header("Upgrade Settings")]
        [SerializeField, Range(0, 1)] private float multiplier = 0.2f;

        [Header("Unlock Condition Settings")]
        [SerializeField] private int cookiesBakedToUnlock;

        public override Upgrade Get()
        {
            var upgrade = new CookiesUpgrade(Percentage.FromFraction(multiplier));
            var condition = new CookiesBakedCondition(cookiesBakedToUnlock);
            
            upgrade.AddUnlockCondition(condition);
            
            return upgrade;
        }
    }
}