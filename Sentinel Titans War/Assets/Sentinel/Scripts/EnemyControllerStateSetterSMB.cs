using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    public class EnemyControllerStateSetterSMB : StateMachineBehaviour
    {
        public string state;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.gameObject.GetComponent<EnemyController>();
            if (controller == null) return;
            controller.state = state;
        }




        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.gameObject.GetComponent<EnemyController>();
            if (controller == null) return;
            controller.state = " ";
        }

    }
}

