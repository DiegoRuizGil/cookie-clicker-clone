using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI amountText;

        public event Action<string> OnButtonPressed = delegate { };
        public event Action<string> OnHoverEnter = delegate { };
        public event Action OnHoverExit = delegate { };
        
        private Button _button;

        public string BuildingName => _displayData.name;
        private BuildingDisplayData _displayData;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnButtonPressed.Invoke(BuildingName));
        }
        
        public void OnPointerEnter(PointerEventData eventData) => OnHoverEnter.Invoke(BuildingName);
        public void OnPointerExit(PointerEventData eventData) => OnHoverExit.Invoke();

        public void UpdateData(BuildingDisplayData displayData)
        {
            _displayData = displayData;
            costText.text = _displayData.cost.ToString($"'x{displayData.purchaseMult}' #");
            amountText.text = _displayData.amount.ToString("#");
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
                    icon.sprite = _displayData.silhouette;
                    break;
                case BuildingVisibility.Revealed:
                    gameObject.SetActive(true);
                    nameText.text = _displayData.name;
                    icon.sprite = _displayData.icon;
                    break;
            }
        }

        public void SetInteraction(float currentCookies, PurchaseMode.Type purchaseType)
        {
            switch (purchaseType)
            {
                case PurchaseMode.Type.Buy:
                    _button.interactable = currentCookies >= _displayData.cost;
                    break;
                case PurchaseMode.Type.Sell:
                    _button.interactable = _displayData.amount > 0;
                    break;
            }
        }
    }
}