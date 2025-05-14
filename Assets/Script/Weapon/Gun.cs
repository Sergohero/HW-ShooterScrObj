using System.Collections.Generic;
using UnityEngine;

namespace HWShoter
{
    public sealed class Gun : Weapon
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _count;
        
        private Transform _bulletRoot;
        private Queue<Bullet> _bulletQueue;

        protected override void Start()
        {
            base.Start();
            _bulletQueue = new Queue<Bullet>(_count);
            _bulletRoot = new GameObject("BulletRoot").transform;
            Recharge();
        }
        
        public override string GetAmmoInfo()
        {
            int bulletsLeft = _bulletQueue?.Count ?? 0;
            return $"Bullets: {bulletsLeft}/{_count}";
        }
        
        public override void Recharge()
        {
            while (_bulletQueue.Count < _count)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
                bullet.Sleep();
                _bulletQueue.Enqueue(bullet);
            }
        }

        public override void Fire()
        {
            if (CanShoot == false)
            {
                return;
            }
            
            if (_bulletQueue.TryDequeue(out Bullet bullet))
            {
               bullet.Run(_barrel.forward * Force, _barrel.position);
               LastShootTime = 0.0f;
            }
        }
    }
}