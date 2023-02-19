namespace Health
{
    public interface IHealthCange
    {
        void OnHealthChanged(int currentHealth, int currentAdditionalHealth, float currentHealthAsPercantage);
    }
}