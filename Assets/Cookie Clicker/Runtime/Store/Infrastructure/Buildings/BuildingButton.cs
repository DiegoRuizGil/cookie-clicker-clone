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
        private BuildingUpdateRequest.Mode _currentMode = BuildingUpdateRequest.Mode.Buy;
        private int _groupAmount = 1;
        
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
            costText.text = _building.CostOf(_groupAmount).ToString($"'{_groupAmount}x' #");
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
    }
}