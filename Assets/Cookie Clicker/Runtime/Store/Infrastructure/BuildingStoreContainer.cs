using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure
{
    public class BuildingStoreContainer : MonoBehaviour
    {
        [SerializeField] private BuildingButton buildingButtonPrefab;
        [SerializeField] private BakerBehaviour bakerBehaviour;

        [SerializeField] private List<BuildingConfig> buildingConfigs;

        private void Start()
        {
            foreach (var buildingConfig in buildingConfigs)
            {
                var button = Instantiate(buildingButtonPrefab, transform);
                button.Init(buildingConfig, bakerBehaviour.Baker);
            }
        }
    }
}