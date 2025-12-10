using Cookie_Clicker.Runtime.Builders;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Effects;
using Cookie_Clicker.Runtime.Modifiers.Domain.Unlock_Conditions;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Tappnig and Cursor")]
    public class CursorUpgradeConfig : BaseUpgradeConfig
    {
        public override UpgradeType Type => UpgradeType.Cursor;
        
        [Header("Upgrade Settings")]
        [SerializeField] private BuildingID cursorID;
        [SerializeField] private float efficiencyMultiplier = 2;
        
        [Header("Unlock Condition Settings")]
        [SerializeField] private int cursorCountToUnlock;

        public override Upgrade Get()
        {
            var effect = new CursorEffect(cursorID, efficiencyMultiplier);
            var condition = new BuildingCountCondition(cursorID, cursorCountToUnlock);
            var upgrade = An.Upgrade.WithName(upgradeName).WithIcon(icon).WithCost(cost)
                .WithDescription(description).WithEffect(effect).Build();
            
            upgrade.AddUnlockCondition(condition);

            return upgrade;
        }
    }
}