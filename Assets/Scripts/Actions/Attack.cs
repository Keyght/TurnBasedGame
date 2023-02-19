namespace Actions
{
    /// <summary>
    /// Класс для действия атаки
    /// </summary>
    public class Attack : BaseAction
    {
        private static int _attackValue = -3;

        public static int AttackValue => _attackValue;

        private new void Start()
        {
            base.Start();
            IsEnemyTargeted = true;
        }

        public override void PerformAction()
        {
            Target.Health.ChangeHealth(_attackValue, IsAttacking);
        }
    }
}