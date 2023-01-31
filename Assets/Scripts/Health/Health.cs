using System;

public class Health
{
    private int _maxHP;
    private int _currentHP;
    private int _additionalHP;
    public event Action<int, int, float> HealthChanged;

    public Health(int maxHP)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
        _additionalHP = 0;
    }

    public void ChangeHealth(int value, bool isDamage)
    {
        if (isDamage)
        {
            if (_additionalHP >= -1 * value)
            {
                _additionalHP += value;
            }
            else
            {
                value += _additionalHP;
                _additionalHP = 0;
            }
        }

        _currentHP += value;
        
        if (_currentHP <= 0)
        {
            Death();
        }
        else if (_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }
        else
        {
            float currentHealthAsPercantage = (float) _currentHP / _maxHP;
            HealthChanged?.Invoke(_currentHP, _additionalHP, currentHealthAsPercantage);
        }
    }

    public void Death()
    {
        HealthChanged?.Invoke(0, 0, 0);
    }

    public int GetCurrentHP()
    {
        return _currentHP;
    }
}
