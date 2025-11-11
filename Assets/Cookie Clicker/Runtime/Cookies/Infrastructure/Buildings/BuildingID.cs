using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings
{
    [CreateAssetMenu(menuName = "Building/ID")]
    public class BuildingID : ScriptableObject
    {
        [SerializeField] private string buildingName;
        
        public static implicit operator string(BuildingID other) => other.buildingName;
    }
}