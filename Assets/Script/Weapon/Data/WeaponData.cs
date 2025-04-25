using UnityEngine;

namespace HWShoter
{
    [CreateAssetMenu(fileName = nameof(WeaponData), menuName = "Data/Weapon/Data")]
    public sealed class WeaponData : ScriptableObject
    {
        [SerializeField] private float _force;
        [SerializeField] private float _shotDelay;
        
        public float Force => _force;
        public float ShotDelay => _shotDelay;
    }
}