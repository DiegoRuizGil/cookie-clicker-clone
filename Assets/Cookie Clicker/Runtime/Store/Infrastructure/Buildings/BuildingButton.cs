using System;
using System.Globalization;
using Cookie_Clicker.Runtime.Cookies.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI amountText;

        public event Action<string> OnButtonPressed = delegate { } ;
        
        private Button _button;

        private BuildingData _buildingData;
        public string BuildingName { get; private set; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnButtonPressed.Invoke(BuildingName));
        }

        public void Init(string buildingName)
        {
            BuildingName = buildingName;
            nameText.text = BuildingName;
        }

        public void UpdateData(BuildingData data)
        {
            _buildingData = data;
            costText.text = _buildingData.cost.ToString($"'x{data.multiplier}' #");
            amountText.text = _buildingData.amount.ToString(CultureInfo.InvariantCulture);
        }

        public void SetInteraction(float currentCookies, PurchaseMode.Type purchaseType)
        {
            switch (purchaseType)
            {
                case PurchaseMode.Type.Buy:
                    _button.interactable = currentCookies >= _buildingData.cost;
                    break;
                case PurchaseMode.Type.Sell:
                    _button.interactable = _buildingData.amount > 0;
                    break;
            }
        }
    }
}