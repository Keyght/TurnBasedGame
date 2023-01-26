using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRandom : StateMachineBehaviour
{
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("DeathID", Random.Range(0, 3));
    }
}
