using System;

public class Health
{
    private int _maxHP;
    private int _currentHP;
    public event Action<int, float> HealthChanged;

    public Health(int maxHP)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
    }

    public void ChangeHealth(int value)
    {
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
            HealthChanged?.Invoke(_currentHP, currentHealthAsPercantage);
        }
    }

    public void Death()
    {
        HealthChanged?.Invoke(0, 0);
    }

    public int GetCurrentHP()
    {
        return _currentHP;
    }
}
