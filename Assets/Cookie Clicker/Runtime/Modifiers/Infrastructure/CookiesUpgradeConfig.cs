using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    [CreateAssetMenu(menuName = "Upgrades/Global Production")]
    public class CookiesUpgradeConfig : BaseUpgradeConfig
    {
        [SerializeField, Range(0, 1)] private float multiplier = 0.2f;
        
        public override void Init()
        {
            Upgrade = new CookiesUpgrade(Percentage.FromFraction(multiplier));
        }
    }
}