using UnityEngine;

namespace AnimatorScripts
{
    /// <summary>
    /// Класс для выхода из состояния анимации каста
    /// </summary>
    public class CastEnd : StateMachineBehaviour
    {
        private static readonly int _cast = Animator.StringToHash("Cast");
        private static readonly int _idle = Animator.StringToHash("Idle");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(_cast);
            animator.SetTrigger(_idle);
        }
    }
}