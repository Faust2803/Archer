using UnityEngine;

namespace Code.PlayerControls
{
    public class AnimationAimingController:StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.gameObject.GetComponent<ArcherController>();
            controller.StartShooting();
        }
    }
}