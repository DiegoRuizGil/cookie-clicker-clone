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

        public void Setup(List<Building> buildings)
        {
            foreach (var building in buildings)
            {
                var button = Instantiate(buildingButtonPrefab, transform);
                button.Init(building.name);
                
                _buttons.Add(button);
            }
        }

        public void UpdateButtonsInteraction(float currentCookies, PurchaseMode.Type purchaseType)
        {
            foreach (var button in _buttons)
                button.SetInteraction(currentCookies, purchaseType);
        }

        public void UpdateButtonsData(List<BuildingData> buildingsData)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (i < buildingsData.Count)
                    _buttons[i].UpdateData(buildingsData[i]);
                else
                    _buttons[i].gameObject.SetActive(false);
            }
        }

        public void RegisterPurchasedModeListener(Action<PurchaseMode> listener)
        {
            modeSelector.OnUpdated += listener;
        }

        public void RegisterButtonClickListener(Action<string> listener)
        {
            foreach (var button in _buttons)
                button.OnButtonPressed += listener;
        }
    }
}