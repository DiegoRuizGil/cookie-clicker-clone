using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Tappnig and Cursor")]
    public class TappingCursorUpgradeConfig : BaseUpgradeConfig
    {
        [SerializeField] private BuildingID cursorID;
        [SerializeField] private float efficiencyMultiplier = 2;
        
        public override void Init()
        {
            Upgrade = new TappingCursorUpgrade(cursorID.buildingName, efficiencyMultiplier);
        }
    }
}