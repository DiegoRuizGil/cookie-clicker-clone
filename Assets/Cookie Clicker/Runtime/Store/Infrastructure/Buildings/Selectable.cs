using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    [RequireComponent(typeof(Image))]
    public class Selectable : MonoBehaviour, IPointerClickHandler
    {
        [Header("Debug")]
        [SerializeField] private Color debugDefaultColor;
        [SerializeField] private Color debugSelectedColor;

        public bool Selected { get; private set; }
        public event Action OnSelected = delegate { };
        
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.color = debugDefaultColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Selected) Select();
        }

        public void Select()
        {
            Selected = true;
            _image.color = debugSelectedColor;
            OnSelected.Invoke();
        }

        public void Deselect()
        {
            Selected = false;
            _image.color = debugDefaultColor;
        }
    }
}