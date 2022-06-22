using UnityEngine;

namespace Code.PlayerControls
{
    public class AnimationShootController:StateMachineBehaviour
    {
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isShooting", false);
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.gameObject.GetComponent<ArcherController>();
            controller.FinishShoot();
        }
    }
}