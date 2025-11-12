using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Tappnig and Cursor")]
    public class TappingCursorUpgradeConfig : BaseUpgradeConfig
    {
        [Header("Upgrade Settings")]
        [SerializeField] private BuildingID cursorID;
        [SerializeField] private float efficiencyMultiplier = 2;
        
        [Header("Unlock Condition Settings")]
        [SerializeField] private int cursorCountToUnlock;

        public override Upgrade Get()
        {
            var upgrade = new TappingCursorUpgrade(cursorID, efficiencyMultiplier);
            var condition = new BuildingCountCondition(cursorID, cursorCountToUnlock);
            
            upgrade.AddUnlockCondition(condition);

            return upgrade;
        }
    }
}