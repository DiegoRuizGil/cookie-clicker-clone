using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Efficiency")]
    public class EfficiencyUpgradeConfig : BaseUpgradeConfig
    {
        [Header("Upgrade Settings")]
        [SerializeField] private BuildingID buildingID;
        [SerializeField] private float efficiencyMult = 2;

        [Header("Unlock Condition Settings")]
        [SerializeField] private int buildingCountToUnlock;

        public override Upgrade Get()
        {
            var upgrade = new EfficiencyUpgrade(buildingID, efficiencyMult);
            var condition = new BuildingCountCondition(buildingID, buildingCountToUnlock);
            
            upgrade.AddUnlockCondition(condition);

            return upgrade;
        }
    }
}