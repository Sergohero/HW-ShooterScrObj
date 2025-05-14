using TMPro;
using UnityEngine;

namespace HWShoter
{
    public class AmmoDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammoText; 
        [SerializeField] private Weapon[] _weapons;

        private void Awake()
        {
            if (_weapons == null || _weapons.Length == 0)
            {
                _weapons = GetComponentsInChildren<Weapon>(true);
            }
        }
        
        private void Update()
        {
            if (_weapons == null || _ammoText == null)
                return;

            // Очистить текст перед обновлением
            _ammoText.text = string.Empty;

            foreach (var weapon in _weapons)
            {
                _ammoText.text += weapon.GetAmmoInfo() + "\n";
            }
        }
    }
}