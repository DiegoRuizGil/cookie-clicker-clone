using Cookie_Clicker.Runtime.Builders;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Buildings;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings
{
    [CreateAssetMenu(menuName = "Building/Config")]
    public class BuildingConfig : ScriptableObject
    {
        public BuildingID buildingID;
        [SerializeField] private double baseCPS;
        [SerializeField, Min(0)] private double baseCost;
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite silhouette;

        public double BaseCost => baseCost;
        
        public Building Get()
        {
            return A.Building.WithName(buildingID)
                .WithBaseCPS(baseCPS)
                .WithBaseCost(baseCost)
                .WithIcon(icon)
                .WithIconSilhouette(silhouette);
        }
    }
}