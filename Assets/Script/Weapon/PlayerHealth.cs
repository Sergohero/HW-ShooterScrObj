#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;

namespace HWShoter
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        private int _currentHealth;

        public delegate void HealthChanged(int current, int max);
        public static event HealthChanged OnHealthChanged;

        private Coroutine _damageOverTimeCoroutine;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
            _damageOverTimeCoroutine = StartCoroutine(ApplyDamageOverTime());
        }

        private IEnumerator ApplyDamageOverTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                if (_currentHealth > 0)
                {
                    TakeDamage(5);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
                _currentHealth = 0;

            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Player is dead!");
            StopCoroutine(_damageOverTimeCoroutine); 
            Quit();
        }
        
        public void Quit()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}