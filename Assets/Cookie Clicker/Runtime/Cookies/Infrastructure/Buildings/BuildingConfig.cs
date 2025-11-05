using Cookie_Clicker.Runtime.Cookies.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings
{
    [CreateAssetMenu(menuName = "Building/Config")]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private BuildingID _buildingID;
        [SerializeField] private float baseCPS;

        public Building Building => _building;
        
        private Building _building;

        public void Init()
        {
            _building = new Building(_buildingID.buildingName, baseCPS);
        }
    }
}