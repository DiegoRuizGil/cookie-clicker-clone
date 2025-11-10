using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    public class BuildingModeSelector : MonoBehaviour
    {
        [Header("Mode buttons")]
        [SerializeField] private Selectable buyModeButton;
        [SerializeField] private Selectable sellModeButton;
        
        [Header("Amount buttons")]
        [SerializeField] private Selectable amount1Button;
        [SerializeField] private Selectable amount10Button;
        [SerializeField] private Selectable amount100Button;

        private BuildingUpdateRequest.Mode _mode;
        private int _groupAmount;

        public event Action<BuildingUpdateRequest.Mode, int> OnUpdated = delegate { };

        private void Start()
        {
            RegisterListeners();
            
            buyModeButton.Select();
            amount1Button.Select();
            UpdateMode(BuildingUpdateRequest.Mode.Buy, 1);
        }

        private void RegisterListeners()
        {
            buyModeButton.OnSelected += () =>
            {
                sellModeButton.Deselect();
                UpdateMode(BuildingUpdateRequest.Mode.Buy, _groupAmount);
            };
            sellModeButton.OnSelected += () =>
            {
                buyModeButton.Deselect();
                UpdateMode(BuildingUpdateRequest.Mode.Sell, _groupAmount);
            };
            
            amount1Button.OnSelected += () =>
            {
                amount10Button.Deselect();
                amount100Button.Deselect();
                UpdateMode(_mode, 1);
            };
            amount10Button.OnSelected += () =>
            {
                amount1Button.Deselect();
                amount100Button.Deselect();
                UpdateMode(_mode, 10);
            };
            amount100Button.OnSelected += () =>
            {
                amount1Button.Deselect();
                amount10Button.Deselect();
                UpdateMode(_mode, 100);
            };
        }

        private void UpdateMode(BuildingUpdateRequest.Mode mode, int groupAmount)
        {
            _mode = mode;
            _groupAmount = groupAmount;
            OnUpdated.Invoke(mode, _groupAmount);
        }
    }
}