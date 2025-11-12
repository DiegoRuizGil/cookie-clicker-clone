using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades;
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
            var upgrade = new GrandmaUpgrade(
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
            
            upgrade.AddUnlockConditions(conditions);
            upgrade.icon = icon;
            
            return upgrade;
        }
    }
}