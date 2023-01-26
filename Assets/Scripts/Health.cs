using System;
using UnityEngine;

public class Health
{
    private const int _maxHP = 10;
    private int _currentHP;
    public event Action<float> HealthChanged;

    public Health()
    {
        _currentHP = _maxHP;
    }

    public void ChangeHealth(int value)
    {
        _currentHP += value;
        
        if (_currentHP <= 0)
        {
            Death();
        }
        else
        {
            float currentHealthAsPercantage = (float) _currentHP / _maxHP;
            HealthChanged?.Invoke(currentHealthAsPercantage);
        }
    }

    public void Death()
    {
        HealthChanged?.Invoke(0);
    }

    public int GetCurrentHP()
    {
        return _currentHP;
    }
}
