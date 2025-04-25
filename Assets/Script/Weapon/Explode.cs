using UnityEngine;

namespace HWShoter
{
    public class Explode : MonoBehaviour
    {
        [SerializeField] private Light _light;

        public void TurnLightOn()
        {
            _light.enabled = true;
            Debug.Log("Turned Light");
        }
    }
}