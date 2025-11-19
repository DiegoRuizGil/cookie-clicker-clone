using System;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private Image icon;

        public event Action<string> OnButtonPressed = delegate { };
        
        private Button _button;
        private UpgradeDisplayData _displayData;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        public void Init(UpgradeDisplayData displayData)
        {
            _displayData = displayData;
            icon.sprite = _displayData.icon;
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
    }
}