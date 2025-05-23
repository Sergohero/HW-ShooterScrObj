﻿using UnityEngine;

namespace HWShoter
{
    public sealed class MK47 : Weapon
    {
        [SerializeField] private Grenade _grenadePrefab;
        
        private Grenade _grenade;

        public override void Fire()
        {
            if (_grenade)
            {
                _grenade.Run(_barrel.forward * Force);
                _grenade = null;
            }
        }

        public override void Recharge()
        {
            if (_grenade != null)
            {
                return;
            }
            _grenade = Instantiate(_grenadePrefab, _barrel);
            _grenade.Sleep(_barrel.position);
        }
    }
}