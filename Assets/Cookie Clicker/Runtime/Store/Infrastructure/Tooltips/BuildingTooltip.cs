using Cookie_Clicker.Runtime.Cookies.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips
{
    public class BuildingTooltip : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI cpsText;
        [SerializeField] private TextMeshProUGUI productionText;
        

        private void Awake()
        {
            Hide();
        }

        public void Show(BuildingDisplayData data, Vector2 position)
        {
            UpdateData(data);
            rectTransform.anchoredPosition = position;
            gameObject.SetActive(true);
        }

        public void UpdateData(BuildingDisplayData data)
        {
            icon.sprite = data.icon;
            nameText.text = data.name;
            amountText.text = data.amount.ToString("'Owned:' #");
            costText.text = data.cost.ToString("#");
            cpsText.text = $"each cursor produces <color=white>{data.cpsPer:F2} cookies</color> per second";
            productionText.text = $"{data.amount} cursors produces <color=white>{data.totalProduction:F2} cookies</color> per second";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}