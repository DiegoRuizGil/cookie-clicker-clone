using System;
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

        private Building _building;
        private BuildingUpdateRequest.Mode _currentMode;
        private int _groupAmount;
        
        public event Action<BuildingUpdateRequest> OnButtonPressed = delegate { } ;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void Start()
        {
            UpdateTexts();
        }

        public void Init(Building building)
        {
            _building = building;
        }

        public void RegisterListener(Action<BuildingUpdateRequest> callback) => OnButtonPressed += callback;

        public void UpdateTexts()
        {
            nameText.text = _building.name;
            costText.text = GetCostText();
            amountText.text = _building.Amount.ToString();
        }

        public void ChangeMode(BuildingUpdateRequest.Mode mode, int groupAmount)
        {
            _currentMode = mode;
            _groupAmount = groupAmount;
        }

        private void OnClick()
        {
            var request = new BuildingUpdateRequest
            {
                building = _building,
                amount = _groupAmount,
                mode = _currentMode
            };
            OnButtonPressed.Invoke(request);
        }

        private string GetCostText()
        {
            var amount = _currentMode switch
            {
                BuildingUpdateRequest.Mode.Buy => _building.CostOf(_groupAmount),
                BuildingUpdateRequest.Mode.Sell => _building.RefoundOf(_groupAmount),
                _ => 0f
            };

            return amount.ToString($"'{_groupAmount}x' #");
        }
    }
}