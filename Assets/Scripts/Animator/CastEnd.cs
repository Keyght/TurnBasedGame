using UnityEngine;

/// <summary>
/// Класс для выхода из состояния анимации каста
/// </summary>
public class CastEnd : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Cast");
    }
}
