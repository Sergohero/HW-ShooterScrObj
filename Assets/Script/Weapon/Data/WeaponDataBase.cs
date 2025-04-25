using System;
using UnityEngine;

namespace HWShoter
{
    [CreateAssetMenu(fileName = nameof(WeaponDataBase), menuName = "Data/Weapon/Type")]
    public sealed class WeaponDataBase : ScriptableObject
    {
        [Serializable]
        private sealed class WeaponLevelData
        {
            public int Level;
            public WeaponData Data;
        }

        [SerializeField] private WeaponLevelData[] _datas;

        public WeaponData GetWeaponData(int level)
        {
            for (var index = 0; index < _datas.Length; index++)
            {
                var data = _datas[index];
                if (data.Level == level)
                {
                    return data.Data;
                }
            }
            return null; 
        }
    }
}