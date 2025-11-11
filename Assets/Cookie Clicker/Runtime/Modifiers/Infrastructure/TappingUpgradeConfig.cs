using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Tapping")]
    public class TappingUpgradeConfig : BaseUpgradeConfig
    {
        [Header("Upgrade Settings")]
        [SerializeField, Range(0, 1)] private float multiplier = 0.01f;

        [Header("Unlock Condition Settings")]
        [SerializeField] private int handMadeCookiesToUnlock;

        public override Upgrade Get()
        {
            var upgrade = new TappingUpgrade(Percentage.FromFraction(multiplier));
            var condition = new HandMadeCookiesCondition(handMadeCookiesToUnlock);
            
            upgrade.AddUnlockCondition(condition);

            return upgrade;
        }
    }
}