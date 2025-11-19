using Cookie_Clicker.Runtime.Modifiers.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips
{
    public class UpgradeTooltip : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI descriptionText;

        private Vector2 _offset;
        
        private void Awake()
        {
            _offset = new Vector2(rectTransform.rect.width / 2, rectTransform.rect.height * 1 / 4);
            Hide();
        }
        
        public void Show(UpgradeDisplayData data, Vector2 position)
        {
            UpdateData(data);
            UpdatePosition(position);
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void UpdateData(UpgradeDisplayData data)
        {
            icon.sprite = data.icon;
            SetTextAndSize(nameText, data.name);
            costText.text = data.cost.ToString("#");
            SetTextAndSize(descriptionText, data.description);
        }

        public void UpdateCostTextColor(bool canPurchase) => costText.color = canPurchase ? Color.green : Color.red;

        private void UpdatePosition(Vector2 position)
        {
            rectTransform.position = position - _offset;
        }

        private void SetTextAndSize(TextMeshProUGUI textMesh, string text)
        {
            textMesh.text = text;
            textMesh.ForceMeshUpdate();
            var rt = textMesh.rectTransform;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, textMesh.preferredHeight);
        }
    }
}