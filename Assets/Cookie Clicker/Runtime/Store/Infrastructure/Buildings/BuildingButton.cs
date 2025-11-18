using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI amountText;

        public event Action<string> OnButtonPressed = delegate { };
        
        private Button _button;

        private float _tooltipXPos;
        private BuildingTooltip _tooltip;
        public string BuildingName => _displayData.name;
        private BuildingDisplayData _displayData;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnButtonPressed.Invoke(BuildingName));
        }

        public void Init(BuildingDisplayData displayData, BuildingTooltip tooltip, float tooltipXPos)
        {
            _tooltip = tooltip;
            _tooltipXPos = tooltipXPos;
            UpdateData(displayData);
        }

        public void UpdateData(BuildingDisplayData displayData)
        {
            _displayData = displayData;
            costText.text = _displayData.cost.ToString($"'x{displayData.purchaseMult}' #");
            amountText.text = _displayData.amount.ToString("#");
            
            _tooltip.UpdateData(displayData);
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
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltip.Show(_displayData, new Vector2(_tooltipXPos, Input.mousePosition.y));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltip.Hide();
        }
        
        public void OnPointerMove(PointerEventData eventData)
        {
            _tooltip.UpdatePosition(new Vector2(_tooltipXPos, Input.mousePosition.y));
        }
    }
}