using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthCanvas : MonoBehaviour, IHealthCange
    {
        [SerializeField] private Image _healthBarFilling;
        [SerializeField] private Image _poisonIcon;
        [SerializeField] private TextMeshProUGUI _healthCount;
        [SerializeField] private TextMeshProUGUI _additionalHealthCount;
        [SerializeField] private Gradient _gradient;

        private Character _character;
        private Health _health;

        private void Start()
        {
            _character = transform.parent.GetComponent<Character>();
            _health = _character.Health;
            _health.HealthChanged += OnHealthChanged;
        }

        public void OnHealthChanged(int currentHealth, int currentAdditionalHealth, float currentHealthAsPercantage)
        {
            _poisonIcon.enabled = _character.Effects.ContainsKey(Effect.Poisoned);

            _healthCount.text = currentHealth.ToString();
            _additionalHealthCount.text = currentAdditionalHealth.ToString();
            _healthCount.color = _gradient.Evaluate(currentHealthAsPercantage);

            _healthBarFilling.fillAmount = currentHealthAsPercantage;
            _healthBarFilling.color = _gradient.Evaluate(currentHealthAsPercantage);
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }
    }
}