using Cookie_Clicker.Runtime.Builders;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Effects;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Global Production")]
    public class CookiesUpgradeConfig : BaseUpgradeConfig
    {
        [Header("Effect Settings")]
        [SerializeField, Range(0, 1)] private float multiplier = 0.2f;

        [Header("Unlock Condition Settings")]
        [SerializeField] private double cookiesBakedToUnlock;

        public override Upgrade Get()
        {
            var effect = new CookiesEffect(Percentage.FromFraction(multiplier));
            var condition = new CookiesBakedCondition(cookiesBakedToUnlock);
            var upgrade = An.Upgrade.WithName(upgradeName).WithIcon(icon).WithCost(cost)
                .WithDescription(description).WithEffect(effect).Build();

            upgrade.AddUnlockCondition(condition);
            
            return upgrade;
        }
    }
}