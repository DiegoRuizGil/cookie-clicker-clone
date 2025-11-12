using System.Collections.Generic;
using Cookie_Clicker.Runtime.Builders;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Effects;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Grandma")]
    public class GrandmaUpgradeConfig : BaseUpgradeConfig
    {
        [Header("Upgrade Settings")]
        [SerializeField] private BuildingID grandmaID;
        [SerializeField] private BuildingID buildingID;
        [SerializeField] private float grandmaEfficiencyMultiplier = 2;
        [SerializeField, Range(0, 1)] private float buildingMultiplier = 0.01f;
        [SerializeField, Min(1)] private int grandmaGroupSize = 1;
        
        [Header("Unlock Condition Settings")]
        [SerializeField] private int grandmaCountToUnlock;
        [SerializeField] private int buildingCountToUnlock;

        public override Upgrade Get()
        {
            var effect = new GrandmaEffect(
                grandmaID,
                buildingID,
                grandmaEfficiencyMultiplier,
                Percentage.FromFraction(buildingMultiplier),
                grandmaGroupSize);
            var conditions = new List<IUnlockCondition>
            {
                new BuildingCountCondition(grandmaID, grandmaCountToUnlock),
                new BuildingCountCondition(buildingID, buildingCountToUnlock)
            };
            var upgrade = An.Upgrade.WithName(upgradeName).WithIcon(icon).WithCost(cost)
                .WithEffect(effect).Build();
            
            upgrade.AddUnlockConditions(conditions);
            
            return upgrade;
        }
    }
}