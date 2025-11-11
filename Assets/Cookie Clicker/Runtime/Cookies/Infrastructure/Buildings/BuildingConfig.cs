using Cookie_Clicker.Runtime.Cookies.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings
{
    [CreateAssetMenu(menuName = "Building/Config")]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private BuildingID buildingID;
        [SerializeField] private float baseCPS;
        [SerializeField, Min(0)] private int baseCost = 5;

        public Building Get() => new Building(buildingID.buildingName, baseCPS, baseCost);
    }
}