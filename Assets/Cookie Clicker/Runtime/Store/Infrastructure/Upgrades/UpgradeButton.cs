using System;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private Image icon;

        private Button _button;
        private Upgrade _upgrade;

        public event Action<Upgrade> OnButtonPressed = delegate { };

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        public void Init(Upgrade upgrade)
        {
            _upgrade = upgrade;
            icon.sprite = _upgrade.icon;
        }
        
        public void RegisterListener(Action<Upgrade> listener) => OnButtonPressed += listener;

        public void SetInteraction(float currentCookies) => _button.interactable = currentCookies >= _upgrade.cost;

        private void OnClick()
        {
            OnButtonPressed.Invoke(_upgrade);
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