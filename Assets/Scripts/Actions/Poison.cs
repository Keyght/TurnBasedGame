using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Класс для действия отравления
    /// </summary>
    public class Poison : BaseAction
    {
        [SerializeField] private int _poisonTunrs = 1;

        private static int _poisonValue = -1;

        public static int PoisonValue => _poisonValue;

        private new void Start()
        {
            base.Start();
            IsEnemyTargeted = true;
        }

        public override void PerformAction()
        {
            if (Target.Effects.ContainsKey(Effect.Poisoned))
            {
                Target.Effects[Effect.Poisoned] += _poisonTunrs;
            }
            else
            {
                Target.Effects[Effect.Poisoned] = _poisonTunrs;
            }

            Target.Health.ChangeHealth(_poisonValue, IsAttacking);
        }
    }
}