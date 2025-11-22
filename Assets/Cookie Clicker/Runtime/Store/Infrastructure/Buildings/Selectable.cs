using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Buildings
{
    [RequireComponent(typeof(Image))]
    public class Selectable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;

        public bool Selected { get; private set; }
        public event Action OnSelected = delegate { };

        public void Select()
        {
            Selected = true;
            text.color = selectedColor;
            OnSelected.Invoke();
        }

        public void Deselect()
        {
            Selected = false;
            text.color = unselectedColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Selected) Select();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Selected)
                text.color = selectedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Selected)
                text.color = unselectedColor;
        }
    }
}