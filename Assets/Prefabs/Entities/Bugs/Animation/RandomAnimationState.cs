using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int randIndex = Random.Range(0, 3);
        Debug.Log(randIndex);
        animator.SetInteger("Index", randIndex);
    }
}
