using System.Collections;
using UnityEngine;

namespace HWShoter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage = 1.0f;
        [SerializeField] private float _force = 3.0f;
        [SerializeField] private float _bulletLifeTime = 5.0f;
        
        private Rigidbody _rb;
        
        public bool IsActive { get; private set; }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        private void OnBecameInvisible()
        {
            if (IsActive == false)
            {
                return;
            }
            
            Destroy(gameObject);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
            if (other.collider.TryGetComponent(out HealthController health))
            {
                if (health.CanTakeDamage(_damage))
                {
                    return;
                }

                if (!other.collider.TryGetComponent(out Rigidbody rb))
                {
                    Debug.Log("RB not found");
                    return;
                }
                rb.AddForce(_rb.velocity * _force, ForceMode.Impulse);


            }
        }

        public void Sleep()
        {
            _rb.Sleep();
            gameObject.SetActive(false);
            IsActive = false;
        }

        public void Run(Vector3 path, Vector3 startPos)
        {
            transform.position = startPos;
            gameObject.SetActive(true);
            _rb.WakeUp();
            _rb.AddForce(path);
            IsActive = true;
            StartCoroutine(BulletLifeTime());
        }

        private IEnumerator BulletLifeTime()
        {
            while (_bulletLifeTime >= 0.0f)
            {
                _bulletLifeTime -= 1.0f;
                yield return new WaitForSeconds(1.0f);
            }
            Destroy(gameObject);
        }
    }
}