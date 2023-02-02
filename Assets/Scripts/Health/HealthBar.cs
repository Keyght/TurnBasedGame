using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour, IHealthCange
{
    [SerializeField]
    private Image _healthBarFilling;
    [SerializeField]
    private Image _PoisonIcon;
    [SerializeField]
    private TextMeshProUGUI _healthCount;
    [SerializeField]
    private TextMeshProUGUI _additionalHealthCount;
    [SerializeField]
    private Gradient _gradient;

    private Character _character;
    private Health _health;

    private void Start()
    {
        _character = transform.parent.GetComponent<Character>();

        _health = _character.GetHealth();
        _health.HealthChanged += OnHealthChanged;
    }

    public void OnHealthChanged(int currentHealth, int currentAdditionalHealth, float currentHealthAsPercantage)
    {
        if (_character.Effects.ContainsKey(Effect.POISONED))
        {
            _PoisonIcon.enabled = true;
        }
        else
        {
            _PoisonIcon.enabled = false;
        }

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
