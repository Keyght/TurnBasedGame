using System;

public class Health
{
    public event Action<int, int, float> HealthChanged;

    private int _maxHP;
    private int _currentHP;
    private int _armour;

    public Health(int maxHP)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
        _armour = 0;
    }

    public void AddArmour(int value)
    {
        _armour += value;
        InvokeChanges();
    }

    public int ReduceArmour(int value)
    {
        var rem = 0;
        if (_armour >= -1 * value)
        {
            _armour += value;
            InvokeChanges();
        }
        else
        {
            value += _armour;
            rem = value;
            _armour = 0;
        }
        return rem;
    }

    public void ChangeHealth(int value, bool isDamage)
    {
        var rem = value;
        if (isDamage && _armour != 0)
        {
            rem = ReduceArmour(value);
        }

        _currentHP += rem;

        if (_currentHP <= 0)
        {
            _currentHP = 0;
            Death();
        }
        else if (_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
            InvokeChanges();
        }
        else
        {
            InvokeChanges();
        }
    }

    private void InvokeChanges()
    {
        float currentHealthAsPercantage = (float)_currentHP / _maxHP;
        HealthChanged?.Invoke(_currentHP, _armour, currentHealthAsPercantage);
    }

    public void Death()
    {
        HealthChanged?.Invoke(0, 0, 0);
    }

    public int GetCurrentHP()
    {
        return _currentHP;
    }

    public int GetCurrentArmour()
    {
        return _armour;
    }
}
