using UnityEngine;

namespace HWShoter
{
    public sealed class WeaponSelector
    {
        private Weapon _currentWeapon;
        private int _currentIndex;
        private readonly Weapon[] _weapons;
        
        public WeaponSelector(Weapon[] weapons)
        {
            _weapons = weapons;
            foreach (var weapon in _weapons)
            {
                weapon.SetActive(false);
            }
        }
        
        public void Fire()
        {
            if (_currentIndex != null)
            {
                _currentWeapon.Fire();
            }
        }
        
        public void Recharge()
        {
            if (_currentIndex != null)
            {
                _currentWeapon.Recharge();
            }
        }
        
        public void Next()
        {
            _currentIndex++;
            SelectWeapon();
        }
        
        public void Upper()
        {
            _currentIndex--;
            SelectWeapon();
        }
        
        private void SelectWeapon()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.SetActive(false);
            }
            int index = Mathf.Abs(_currentIndex % _weapons.Length);
            _currentWeapon = _weapons[index];
            _currentWeapon.SetActive(true);
        }
    }
}