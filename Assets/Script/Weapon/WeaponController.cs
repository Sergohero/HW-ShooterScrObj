using UnityEngine;

namespace HWShoter
{
    public sealed class WeaponController : MonoBehaviour
    {
        private WeaponSelector _weaponSelector;

        private void Start()
        {
            Weapon[] weapons = GetComponentsInChildren<Weapon>(true);
            
            _weaponSelector = new WeaponSelector(weapons);
        }

        private void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            
            if (scroll >= 0.1f)
            {
                _weaponSelector.Next();
            }
            if (scroll <= -0.1f)
            {
                _weaponSelector.Upper();
            }
            if (Input.GetMouseButton(0))
            {
                _weaponSelector.Fire();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _weaponSelector.Recharge();
            }
            
        }
    }
}