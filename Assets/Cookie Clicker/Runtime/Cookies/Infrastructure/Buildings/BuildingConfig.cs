using Cookie_Clicker.Runtime.Builders;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Buildings;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings
{
    [CreateAssetMenu(menuName = "Building/Config")]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private BuildingID buildingID;
        [SerializeField] private float baseCPS;
        [SerializeField, Min(0)] private int baseCost = 5;
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite silhouette;

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