using UnityEngine;

namespace HWShoter
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _barrel;
        [SerializeField] protected int _level;
        [SerializeField] WeaponDataBase _weaponDataBase;
       
        private float _shotDelay;
        protected float Force { get; private set; }
        public float LastShootTime { get; protected set; }
        protected bool CanShoot {get; private set;}
        
        protected virtual void Start()
        { 
            WeaponData weaponData = _weaponDataBase.GetWeaponData(_level);
            
            Force = weaponData.Force;
            _shotDelay = weaponData.ShotDelay;
        }

        private void Update()
        {
            CanShoot = _shotDelay <= LastShootTime;
            if (CanShoot)
            {
                return;
            }
            
            LastShootTime += Time.deltaTime;
        }

        public abstract void Fire();

        public abstract void Recharge();

        public void SetActive(bool p0)
        {
            gameObject.SetActive(p0);
        }

        public abstract string GetAmmoInfo();
    }
}