using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _healthBarFilling;
    [SerializeField]
    private Character _character;
    [SerializeField]
    private Gradient _gradient;

    private Health _health;

    private void Start()
    {
        _health = _character.GetHealth();
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsPercantage)
    {
        _healthBarFilling.fillAmount = valueAsPercantage;
        _healthBarFilling.color = _gradient.Evaluate(valueAsPercantage);
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

}
