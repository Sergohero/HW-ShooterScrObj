using UnityEngine;

namespace HWShoter
{
    public sealed class Gun : Weapon
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _count;
        
        private Transform _bulletRoot;
        private Bullet[] _bullets;

        protected override void Start()
        {
            base.Start();
            _bulletRoot = new GameObject("BulletRoot").transform;
            Recharge();
        }
        

        public override void Recharge()
        {
            _bullets = new Bullet[_count];
            for (int i = 0; i < _count; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
                bullet.Sleep();
                _bullets[i] = bullet;
            }
        }

        public override void Fire()
        {
            if (CanShoot == false)
            {
                return;
            }
            
            if (TryGetBullet(out Bullet bullet))
            {
               bullet.Run(_barrel.forward * Force, _barrel.position);
               LastShootTime = 0.0f;
            }
        }

        private bool TryGetBullet(out Bullet result)
        {
            int candidate = -1;

            if (_bullets == null)
            {
                result = default;
                return false;
            }

            for (var i = 0; i < _bullets.Length; i++)
            {
                Bullet bullet = _bullets[i];
                if (_bullets[i] == null)
                {
                    continue;
                }

                if (bullet.IsActive)
                {
                    continue;
                }
                
                candidate = i;
                break;

            }
            
            if (candidate == -1)
            {
                result = default;
                return false;
            }
            
            result = _bullets[candidate];
            return true;
        }
    }
}