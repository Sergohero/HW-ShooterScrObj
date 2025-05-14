using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HWShoter
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _healthText;

        private void OnEnable()
        {
            PlayerHealth.OnHealthChanged += UpdateHealthUI;
        }

        private void OnDisable()
        {
            PlayerHealth.OnHealthChanged -= UpdateHealthUI;
        }

        private void UpdateHealthUI(int current, int max)
        {
            if (_healthSlider)
            {
                _healthSlider.maxValue = max;
                _healthSlider.value = current;
            }

            if (_healthText)
            {
                _healthText.text = $"{current}/{max}";
            }
        }
    }
}