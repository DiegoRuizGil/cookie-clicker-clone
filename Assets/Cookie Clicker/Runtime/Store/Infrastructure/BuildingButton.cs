using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure
{
    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI amountText;
        
        private BuildingConfig _buildingConfig;
        private CookieBaker _baker;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void Start()
        {
            UpdateTexts();
        }

        public void Init(BuildingConfig config, CookieBaker baker)
        {
            _buildingConfig = config;
            _baker = baker;
            
            _buildingConfig.Init();
        }

        private void OnClick()
        {
            _baker.AddBuilding(_buildingConfig.Building);
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            nameText.text = _buildingConfig.Building.name;
            amountText.text = _buildingConfig.Building.Amount.ToString();
        }
    }
}