using Cookie_Clicker.Runtime.Builders;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Effects;
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
        [SerializeField] private double handMadeCookiesToUnlock;

        public override Upgrade Get()
        {
            var effect = new TappingEffect(Percentage.FromFraction(multiplier));
            var condition = new HandMadeCookiesCondition(handMadeCookiesToUnlock);
            var upgrade = An.Upgrade.WithName(upgradeName).WithIcon(icon).WithCost(cost)
                .WithDescription(description).WithEffect(effect).Build();
            
            upgrade.AddUnlockCondition(condition);

            return upgrade;
        }
    }
}