using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Buildings;
using Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    public class BuildingStoreView : MonoBehaviour, IBuildingStoreView
    {
        [SerializeField] private BuildingModeSelector modeSelector;
        [SerializeField] private BuildingButton buildingButtonPrefab;
        [SerializeField] private BuildingTooltip buildingTooltip;
        
        private readonly List<BuildingButton> _buttons = new List<BuildingButton>();

        public void Setup(List<BuildingDisplayData> displayDataList)
        {
            var rt = GetComponent<RectTransform>();
            var minXPos = rt.TransformPoint(new Vector3(rt.rect.xMin, 0, 0)).x;
            foreach (var data in displayDataList)
            {
                var button = Instantiate(buildingButtonPrefab, transform);
                button.Init(data, buildingTooltip, minXPos);
                button.gameObject.SetActive(false);
                
                _buttons.Add(button);
            }
        }

        public void UpdateButtonsData(List<BuildingDisplayData> buildingsData)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (i < buildingsData.Count)
                    _buttons[i].UpdateData(buildingsData[i]);
                else
                    _buttons[i].gameObject.SetActive(false);
            }
        }

        public void UpdateButtonData(string buildingName, BuildingDisplayData displayData)
        {
            var button = _buttons.Find(b => b.BuildingName == buildingName);
            if (button)
                button.UpdateData(displayData);
        }
        
        public void UpdateButtonsInteraction(double currentCookies, PurchaseMode.Type purchaseType)
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
    }
}