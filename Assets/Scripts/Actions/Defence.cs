using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Класс для действия защиты
    /// </summary>
    public class Defence : BaseAction
    {
        [SerializeField] private int _defenceTurns = 3;

        private static readonly int _defence = Animator.StringToHash("Defence");
        private static int _defenceValue = 2;

        public static int DefenceValue => _defenceValue;

        private new void Start()
        {
            base.Start();
            IsSelfTargeted = true;
        }

        public override void PerformAction()
        {
            Target.AnimCtrl.SetTrigger(_defence);
            if (Target.Effects.ContainsKey(Effect.Defended))
            {
                Target.Effects[Effect.Defended] += _defenceTurns;
            }
            else
            {
                Target.Effects[Effect.Defended] = _defenceTurns;
            }

            Target.Health.Armour.AddArmour(_defenceValue);
        }
    }
}