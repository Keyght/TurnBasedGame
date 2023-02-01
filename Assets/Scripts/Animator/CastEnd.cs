using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastEnd : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Cast");
    }
}
