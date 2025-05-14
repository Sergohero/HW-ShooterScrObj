using UnityEngine;

namespace HWShoter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Grenade : MonoBehaviour
    {
        [SerializeField] private float _power;
        [SerializeField] private float _scale;
        
        private Explode _explode;
        private Rigidbody _rb;
        private Collider[] _colliderObj;

        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody>();
            _colliderObj = new Collider[128];
            _explode = GetComponentInChildren<Explode>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject, 0.1f);
            float radius = _scale * 0.5f;
            Vector3 center = other.contacts[0].point;
            _explode.TurnLightOn();
            int countCollied = Physics.OverlapSphereNonAlloc(center, radius, _colliderObj);

            for (int i = 0; i < countCollied; i++)
            {
                Collider collider = _colliderObj[i];
                if (collider.TryGetComponent(out HealthController healthController))
                {
                    if (healthController.CanTakeDamage(healthController.MaxHealth))
                    {
                        return;
                    }

                    if (!healthController.TryGetComponent(out Rigidbody rigidbody))
                    {
                        Debug.Log("RB not found");
                        return;
                    }
                    rigidbody.AddExplosionForce(_power, center, radius);
                }
            }
        }
        public void Run(Vector3 path)
        {
            transform.SetParent(null);
            _rb.WakeUp();
            _rb.isKinematic = false;
            _rb.AddForce(path, ForceMode.Impulse);
        }
        public void Sleep(Vector3 direction)
        {
            _rb.Sleep();
            _rb.isKinematic = true;
            transform.position = direction;
        }
    }
}