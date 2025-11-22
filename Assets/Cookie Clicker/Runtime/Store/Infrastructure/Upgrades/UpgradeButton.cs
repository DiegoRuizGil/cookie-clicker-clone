using System;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private GameObject disablePanel;

        public event Action<string> OnButtonPressed = delegate { };
        
        private Button _button;
        private bool CanPurchase => _button.interactable;
        
        private UpgradeDisplayData _displayData;
        
        private float _tooltipXPos;
        private UpgradeTooltip _tooltip;

        private bool _showingTooltip;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void Update()
        {
            if (_showingTooltip)
                _tooltip.UpdateCostTextColor(CanPurchase);
        }

        public void Init(UpgradeDisplayData displayData, UpgradeTooltip tooltip, float tooltipXPos)
        {
            _displayData = displayData;
            icon.sprite = _displayData.icon;

            _tooltip = tooltip;
            _tooltipXPos = tooltipXPos;
            
            disablePanel.SetActive(!_button.interactable);
        }
        
        public void RegisterListener(Action<string> listener) => OnButtonPressed += listener;

        public void SetInteraction(float currentCookies)
        {
            _button.interactable = currentCookies >= _displayData.cost;
            disablePanel.SetActive(!_button.interactable);
        }

        private void OnClick()
        {
            OnButtonPressed.Invoke(_displayData.name);
            _tooltip.Hide();
            Destroy(gameObject);
        }

        private void OnDestroy() => CleanUp();

        private void CleanUp()
        {
            OnButtonPressed = null;
            _button.onClick.RemoveAllListeners();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltip.Show(_displayData, new Vector2(_tooltipXPos, transform.position.y + 15));
            _showingTooltip = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltip.Hide();
            _showingTooltip = false;
        }
    }
}