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
                button.gameObject.SetActive(false);
                
                _buttons.Add(button);
            }
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

        public void UpdateButtonData(string buildingName, BuildingData data)
        {
            var button = _buttons.Find(b => b.BuildingName == buildingName);
            if (button)
                button.UpdateData(data);
        }
        
        public void UpdateButtonsInteraction(float currentCookies, PurchaseMode.Type purchaseType)
        {
            foreach (var button in _buttons)
                button.SetInteraction(currentCookies, purchaseType);
        }

        public void UpdateVisibility(string buildingName, BuildingVisibility visibility)
        {
            var button = _buttons.Find(b => b.BuildingName == buildingName);
            if (button)
                button.UpdateVisibility(visibility);
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

        public void RegisterButtonHoverListeners(Action<string> onEnter, Action onExit)
        {
            foreach (var button in _buttons)
            {
                button.OnHoverEnter += onEnter;
                button.OnHoverExit += onExit;
            }
        }
    }
}