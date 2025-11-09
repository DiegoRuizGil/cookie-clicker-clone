using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    public class BuildingStoreView : MonoBehaviour, IBuildingStoreView
    {
        [SerializeField] private BuildingButton buildingButtonPrefab;
        
        private readonly List<BuildingButton> _buttons = new List<BuildingButton>();
        
        public void Setup(List<Building> buildings)
        {
            foreach (var building in buildings)
            {
                var button = Instantiate(buildingButtonPrefab, transform);
                button.Init(building);
                
                _buttons.Add(button);
            }
        }

        public void RegisterListener(Action<BuildingBuyRequest> callback)
        {
            foreach (var button in _buttons)
                button.RegisterListener(callback);
        }

        public void UpdateButtons()
        {
            foreach (var button in _buttons)
                button.UpdateTexts();
        }
    }
}