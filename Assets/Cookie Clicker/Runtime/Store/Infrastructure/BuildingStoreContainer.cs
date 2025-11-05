using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure
{
    public class BuildingStoreContainer : MonoBehaviour
    {
        [SerializeField] private BuildingButton _buildingButtonPrefab;

        [SerializeField] private List<BuildingConfig> _buildingConfigs;

        private void Awake()
        {
            foreach (var buildingConfig in _buildingConfigs)
            {
                var button = Instantiate(_buildingButtonPrefab, transform);
                button.Init(buildingConfig, null);
            }
        }
    }
}