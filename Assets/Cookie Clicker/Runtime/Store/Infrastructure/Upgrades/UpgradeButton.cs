using Cookie_Clicker.Runtime.Cookies.Domain;
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
        private CookieBaker _baker;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        public void Init(Upgrade upgrade, CookieBaker baker)
        {
            _upgrade = upgrade;
            _baker = baker;
            
            icon.sprite = _upgrade.icon;
        }

        private void OnClick()
        {
            _upgrade.Apply(_baker);
            Destroy(gameObject);
        }
    }
}