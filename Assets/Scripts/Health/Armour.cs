namespace Health
{
    public class Armour
    {
        public delegate void Invoker();
        public Invoker InvokeChanges;

        private int _value;

        public int Value => _value;

        public Armour()
        {
            _value = 0;
        }

        public void AddArmour(int value)
        {
            _value += value;
            InvokeChanges();
        }

        public int ReduceArmour(int value)
        {
            var rem = 0;
            if (_value >= -1 * value)
            {
                _value += value;
            }
            else
            {
                value += _value;
                rem = value;
                _value = 0;
            }

            InvokeChanges();
            return rem;
        }
    }
}