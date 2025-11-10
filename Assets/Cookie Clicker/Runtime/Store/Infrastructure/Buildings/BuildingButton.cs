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
        
        public event Action<BuildingBuyRequest> OnButtonPressed = delegate { } ;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                var request = new BuildingBuyRequest { building = _building, amount = 1 };
                OnButtonPressed.Invoke(request);
            });
        }

        private void Start()
        {
            UpdateTexts();
        }

        public void Init(Building building)
        {
            _building = building;
        }

        public void RegisterListener(Action<BuildingBuyRequest> callback) => OnButtonPressed += callback;

        public void UpdateTexts()
        {
            nameText.text = _building.name;
            costText.text = _building.CostOf(1).ToString("#");
            amountText.text = _building.Amount.ToString();
        }
    }
}