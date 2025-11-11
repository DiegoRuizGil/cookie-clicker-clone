using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    public class BuildingStoreView : MonoBehaviour, IBuildingStoreView
    {
        [SerializeField] private BuildingModeSelector modeSelector;
        [SerializeField] private BuildingButton buildingButtonPrefab;
        
        private readonly List<BuildingButton> _buttons = new List<BuildingButton>();

        private void Awake()
        {
            modeSelector.OnUpdated += ChangeMode;
        }

        public void Setup(List<Building> buildings)
        {
            foreach (var building in buildings)
            {
                var button = Instantiate(buildingButtonPrefab, transform);
                button.Init(building);
                
                _buttons.Add(button);
            }
        }

        public void RegisterListener(Action<BuildingUpdateRequest> callback)
        {
            foreach (var button in _buttons)
                button.RegisterListener(callback);
        }

        public void UpdateButtons()
        {
            foreach (var button in _buttons)
                button.UpdateTexts();
        }

        private void ChangeMode(BuildingUpdateRequest.Mode mode, int groupAmount)
        {
            foreach (var button in _buttons)
            {
                button.ChangeMode(mode, groupAmount);
                button.UpdateTexts();
            }
        }
    }
}