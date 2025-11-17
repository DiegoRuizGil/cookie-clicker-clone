using System;
using System.Globalization;
using Cookie_Clicker.Runtime.Cookies.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI amountText;

        public event Action<string> OnButtonPressed = delegate { } ;
        public event Action<string> OnHoverEnter = delegate { } ;
        public event Action OnHoverExit = delegate { } ;
        
        private Button _button;

        public string BuildingName { get; private set; }
        private BuildingData _buildingData;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnButtonPressed.Invoke(BuildingName));
        }
        
        public void OnPointerEnter(PointerEventData eventData) => OnHoverEnter.Invoke(BuildingName);
        public void OnPointerExit(PointerEventData eventData) => OnHoverExit.Invoke();

        public void Init(string buildingName)
        {
            BuildingName = buildingName;
            nameText.text = BuildingName;
        }

        public void UpdateData(BuildingData data)
        {
            _buildingData = data;
            costText.text = _buildingData.cost.ToString($"'x{data.multiplier}' #");
            amountText.text = _buildingData.amount.ToString("#");
        }

        public void UpdateVisibility(BuildingVisibility visibility)
        {
            switch (visibility)
            {
                case BuildingVisibility.Hidden:
                    gameObject.SetActive(false);
                    break;
                case BuildingVisibility.NotRevealed:
                    gameObject.SetActive(true);
                    _button.interactable = false;
                    nameText.text = "???";
                    break;
                case BuildingVisibility.Revealed:
                    gameObject.SetActive(true);
                    nameText.text = BuildingName;
                    break;
            }
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