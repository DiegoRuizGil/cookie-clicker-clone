using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradesLayout : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("References")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private RectTransform content;

        [Header("Sizes")]
        [SerializeField] private float collapsedHeight = 80f;
        
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            SetHeight(LayoutUtility.GetPreferredHeight(content));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHeight(collapsedHeight);
        }

        private void SetHeight(float height)
        {
            var size = panel.sizeDelta;
            size.y = height;
            panel.sizeDelta = size;
        }
    }
}