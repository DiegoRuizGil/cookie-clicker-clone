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

        public event Action<BuildingUpdateRequest> OnButtonPressed = delegate { } ;
        
        private Building _building;
        private Button _button;
        
        private BuildingUpdateRequest.Mode _currentMode;
        private int _groupAmount;
        private float _cost;
        

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        public void Init(Building building)
        {
            _building = building;
            UpdateTexts();
        }

        public void RegisterListener(Action<BuildingUpdateRequest> callback) => OnButtonPressed += callback;

        public void UpdateTexts()
        {
            nameText.text = _building.name;
            costText.text = _cost.ToString($"'x{_groupAmount}' #");
            amountText.text = _building.Amount.ToString();
        }

        public void SetInteraction(float currentCookies) => _button.interactable = currentCookies >= _cost;

        public void ChangeMode(BuildingUpdateRequest.Mode mode, int groupAmount)
        {
            _currentMode = mode;
            _groupAmount = groupAmount;
            _cost = _currentMode switch
            {
                BuildingUpdateRequest.Mode.Buy => _building.CostOf(_groupAmount),
                BuildingUpdateRequest.Mode.Sell => _building.RefoundOf(_groupAmount),
                _ => 0f
            };
        }

        private void OnClick()
        {
            var request = new BuildingUpdateRequest
            {
                building = _building,
                amount = _groupAmount,
                mode = _currentMode,
                cost = _cost
            };
            OnButtonPressed.Invoke(request);
        }
    }
}