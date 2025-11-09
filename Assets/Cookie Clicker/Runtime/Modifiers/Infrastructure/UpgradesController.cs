using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    public class UpgradesController
    {
        private readonly List<Upgrade> _unlockedUpgrades = new List<Upgrade>();
        private readonly List<Upgrade> _lockedUpgrades = new List<Upgrade>();
        
        private readonly CookieBaker _baker;

        public UpgradesController(List<Upgrade> upgrades, CookieBaker baker)
        {
            _lockedUpgrades.AddRange(upgrades);
            _baker = baker;
        }

        public void CheckUnlocks()
        {
            var newlyUnlocked = _lockedUpgrades
                .Where(upgrade => upgrade.CanUnlock(_baker))
                .ToList();
            
            newlyUnlocked.ForEach(upgrade =>
            {
                upgrade.Unlock();
                _unlockedUpgrades.Add(upgrade);
            });
            
            _lockedUpgrades.RemoveAll(upgrade => newlyUnlocked.Contains(upgrade));
        }
    }
}