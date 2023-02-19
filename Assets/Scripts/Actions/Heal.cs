using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Класс для действия лечения
    /// </summary>
    public class Heal : BaseAction
    {
        private static readonly int _cast = Animator.StringToHash("Cast");
        private static int _healValue = 1;

        public static int HealValue => _healValue;

        private new void Start()
        {
            base.Start();
            IsSelfTargeted = true;
        }

        public override void PerformAction()
        {
            Target.AnimCtrl.SetTrigger(_cast);
            Target.Effects.Remove(Effect.Poisoned);
            Target.Health.ChangeHealth(_healValue, IsAttacking);
        }
    }
}