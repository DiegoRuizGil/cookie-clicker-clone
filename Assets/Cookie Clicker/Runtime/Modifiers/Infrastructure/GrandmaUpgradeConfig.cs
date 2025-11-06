using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Grandma")]
    public class GrandmaUpgradeConfig : BaseUpgradeConfig
    {
        [SerializeField] private BuildingID grandmaID;
        [SerializeField] private BuildingID buildingID;
        [SerializeField] private float grandmaEfficiencyMultiplier = 2;
        [SerializeField, Range(0, 1)] private float buildingMultiplier = 0.01f;
        [SerializeField, Min(1)] private int grandmaGroupSize = 1;
        
        public override void Init()
        {
            Upgrade = new GrandmaUpgrade(
                grandmaID.buildingName,
                buildingID.buildingName,
                grandmaEfficiencyMultiplier,
                Percentage.FromFraction(buildingMultiplier),
                grandmaGroupSize);
        }
    }
}