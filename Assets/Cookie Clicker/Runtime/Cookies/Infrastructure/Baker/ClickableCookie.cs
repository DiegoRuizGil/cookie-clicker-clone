using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class ClickableCookie : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClick = () => { };
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
        }
    }
}