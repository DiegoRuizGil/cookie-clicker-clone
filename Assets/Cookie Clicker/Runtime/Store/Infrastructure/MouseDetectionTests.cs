using UnityEngine;
using UnityEngine.EventSystems;

namespace Cookie_Clicker.Runtime.Store.Infrastructure
{
    public class MouseDetectionTests : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("On Pointer Enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("On Pointer Exit");
        }
    }
}