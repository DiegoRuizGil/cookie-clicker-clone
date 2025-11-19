using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class UpgradesUnlocker
    {
        public List<Upgrade> LastUpgradesUnlocked { get; private set; }
        public bool NewUnlocksInLastCheck => LastUpgradesUnlocked.Count > 0;
        
        private readonly List<Upgrade> _unlockedUpgrades = new List<Upgrade>();
        private readonly List<Upgrade> _lockedUpgrades = new List<Upgrade>();
        
        private readonly CookieBaker _baker;

        public UpgradesUnlocker(List<Upgrade> upgrades, CookieBaker baker)
        {
            _lockedUpgrades.AddRange(upgrades);
            _baker = baker;
        }

        public Upgrade FindUnlockedUpgrade(string upgradeName)
        {
            return _unlockedUpgrades.Find(upgrade => upgrade.name == upgradeName);
        }
        
        public void CheckUnlocks()
        {
            LastUpgradesUnlocked = GetUnlockedUpgrades();
            if (!NewUnlocksInLastCheck) return;
            
            UpdateUnlockedList();
            UpdateLockedList();
        }

        private List<Upgrade> GetUnlockedUpgrades()
        {
            return _lockedUpgrades
                .Where(upgrade => upgrade.CanUnlock(_baker))
                .ToList();
        }
        
        private void UpdateUnlockedList()
        {
            LastUpgradesUnlocked.ForEach(upgrade =>
            {
                upgrade.Unlock();
                _unlockedUpgrades.Add(upgrade);
            });
        }
        
        private void UpdateLockedList()
        {
            _lockedUpgrades.RemoveAll(upgrade => LastUpgradesUnlocked.Contains(upgrade));
        }
    }
}