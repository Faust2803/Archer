using UnityEngine;

namespace Code.PlayerControls
{
    public class AnimationKickController:StateMachineBehaviour
    {
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isKick", false);
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.gameObject.GetComponent<ArcherController>();
            controller.StopKick();
        }
    }
}