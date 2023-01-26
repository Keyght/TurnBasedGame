using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour, IHealthCange
{
    [SerializeField]
    private Image _healthBarFilling;
    [SerializeField]
    private TextMeshProUGUI _healthCount;
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

    public void OnHealthChanged(int currentHealth, float currentHealthAsPercantage)
    {
        _healthCount.text = currentHealth.ToString();
        _healthCount.color = _gradient.Evaluate(currentHealthAsPercantage);

        _healthBarFilling.fillAmount = currentHealthAsPercantage;
        _healthBarFilling.color = _gradient.Evaluate(currentHealthAsPercantage);
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

}
