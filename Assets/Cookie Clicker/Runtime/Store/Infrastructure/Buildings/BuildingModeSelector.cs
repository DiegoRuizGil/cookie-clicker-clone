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
        
        private PurchaseMode _mode;
        
        public event Action<PurchaseMode> OnUpdated = delegate { };
        
        private void Start()
        {
            RegisterListeners();
            
            buyModeButton.Select();
            amount1Button.Select();
            UpdateMode(new PurchaseMode { type = PurchaseMode.Type.Buy, multiplier = 1});
        }
        
        private void RegisterListeners()
        {
            buyModeButton.OnSelected += () =>
            {
                sellModeButton.Deselect();
                UpdateMode(new PurchaseMode { type = PurchaseMode.Type.Buy, multiplier = _mode.multiplier});
            };
            sellModeButton.OnSelected += () =>
            {
                buyModeButton.Deselect();
                UpdateMode(new PurchaseMode { type = PurchaseMode.Type.Sell, multiplier = _mode.multiplier});
            };
            
            amount1Button.OnSelected += () =>
            {
                amount10Button.Deselect();
                amount100Button.Deselect();
                UpdateMode(new PurchaseMode { type = _mode.type, multiplier = 1});
            };
            amount10Button.OnSelected += () =>
            {
                amount1Button.Deselect();
                amount100Button.Deselect();
                UpdateMode(new PurchaseMode { type = _mode.type, multiplier = 10});
            };
            amount100Button.OnSelected += () =>
            {
                amount1Button.Deselect();
                amount10Button.Deselect();
                UpdateMode(new PurchaseMode { type = _mode.type, multiplier = 100});
            };
        }
        
        private void UpdateMode(PurchaseMode mode)
        {
            _mode = mode;
            OnUpdated.Invoke(_mode);
        }
    }
}