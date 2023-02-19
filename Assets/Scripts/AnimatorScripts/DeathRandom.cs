using UnityEngine;

namespace AnimatorScripts
{
    /// <summary>
    /// Класс для определения анимации смерти
    /// </summary>
    public class DeathRandom : StateMachineBehaviour
    {
        private static readonly int _deathID = Animator.StringToHash("DeathID");

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            animator.SetInteger(_deathID, Random.Range(0, 3));
        }
    }
}