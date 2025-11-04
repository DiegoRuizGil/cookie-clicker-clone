using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class TappingCursorUpgrade : IUpgrade
    {
        private readonly string _cursorName;
        private readonly float _multiplier;
        
        public TappingCursorUpgrade(string cursorName, float multiplier)
        {
            _cursorName = cursorName;
            _multiplier = multiplier;
        }
        
        public void Apply(CookieBaker baker)
        {
            var cursor = baker.FindBuilding(_cursorName);
            if (cursor != null)
            {
                baker.tapMultiplier *= _multiplier;
                cursor.cps.AddEfficiency(_multiplier);
            }
        }
    }
}