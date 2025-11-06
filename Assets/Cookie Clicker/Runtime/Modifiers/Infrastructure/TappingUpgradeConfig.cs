using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Tapping")]
    public class TappingUpgradeConfig : BaseUpgradeConfig
    {
        [SerializeField, Range(0, 1)] private float multiplier = 0.01f;
        
        public override void Init()
        {
            Upgrade = new TappingUpgrade(Percentage.FromFraction(multiplier));
        }
    }
}