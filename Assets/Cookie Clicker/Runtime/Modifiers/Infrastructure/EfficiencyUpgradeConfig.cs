using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Efficiency")]
    public class EfficiencyUpgradeConfig : BaseUpgradeConfig
    {
        [SerializeField] private BuildingID buildingID;
        [SerializeField] private float efficiencyMult = 2;
        
        public override void Init()
        {
            Upgrade = new EfficiencyUpgrade(buildingID.buildingName, efficiencyMult);
        }
    }
}