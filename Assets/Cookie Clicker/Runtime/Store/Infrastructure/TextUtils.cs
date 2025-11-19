namespace Cookie_Clicker.Runtime.Store.Infrastructure
{
    public static class TextUtils
    {
        public static string SetInteractionTextColor(string text, bool canPurchase)
        {
            return canPurchase ? $"<color=green>{text}</color>" : $"<color=red>{text}</color>";
        }
    }
}