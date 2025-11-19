using System;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        [SerializeField] private Image icon;

        public event Action<string> OnButtonPressed = delegate { };
        
        private Button _button;
        
        private UpgradeDisplayData _displayData;
        
        private float _tooltipXPos;
        private UpgradeTooltip _tooltip;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        public void Init(UpgradeDisplayData displayData, UpgradeTooltip tooltip, float tooltipXPos)
        {
            _displayData = displayData;
            icon.sprite = _displayData.icon;

            _tooltip = tooltip;
            _tooltipXPos = tooltipXPos;
        }
        
        public void RegisterListener(Action<string> listener) => OnButtonPressed += listener;

        public void SetInteraction(float currentCookies) => _button.interactable = currentCookies >= _displayData.cost;

        private void OnClick()
        {
            OnButtonPressed.Invoke(_displayData.name);
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