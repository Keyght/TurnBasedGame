using System;

namespace Health
{
    public class Health
    {
        public event Action<int, int, float> HealthChanged;

        private int _maxHp;
        private int _currentHp;
        private Armour _armour;

        public int CurrentHp => _currentHp;
        public Armour Armour => _armour;

        public Health(int maxHp)
        {
            _maxHp = maxHp;
            _currentHp = maxHp;
            _armour = new Armour
            {
                InvokeChanges = InvokeChanges
            };
        }

        public void ChangeHealth(int value, bool isDamage)
        {
            var rem = value;
            if (isDamage && _armour.Value != 0)
            {
                rem = Armour.ReduceArmour(value);
            }

            _currentHp += rem;

            if (_currentHp <= 0)
            {
                _currentHp = 0;
                Death();
            }
            else if (_currentHp > _maxHp)
            {
                _currentHp = _maxHp;
                InvokeChanges();
            }
            else
            {
                InvokeChanges();
            }
        }

        private void InvokeChanges()
        {
            var currentHealthAsPercantage = (float)_currentHp / _maxHp;
            HealthChanged?.Invoke(_currentHp, _armour.Value, currentHealthAsPercantage);
        }

        private void Death()
        {
            HealthChanged?.Invoke(0, 0, 0);
        }
    }
}