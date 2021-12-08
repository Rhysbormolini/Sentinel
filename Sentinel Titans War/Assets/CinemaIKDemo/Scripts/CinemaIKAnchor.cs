using UnityEngine;

namespace NorthLab.CinemaIKDemo
{
    [ExecuteInEditMode]
    public class CinemaIKAnchor : MonoBehaviour
    {

        private Animator animator;
        private IKData ikData;

        private void OnEnable()
        {
            if (!animator)
            {
                animator = GetComponent<Animator>();
                ikData = new IKData();
            }
        }

        private void OnAnimatorIK()
        {
            if (animator && ikData != null)
            {
                animator.SetLookAtWeight(ikData.weight);
                animator.SetLookAtPosition(ikData.lookAtPos);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikData.leftHandPosWeight);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, ikData.leftHandPos);

                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikData.leftHandRotWeight);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, ikData.leftHandRot);

                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikData.rightHandPosWeight);
                animator.SetIKPosition(AvatarIKGoal.RightHand, ikData.rightHandPos);

                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikData.rightHandRotWeight);
                animator.SetIKRotation(AvatarIKGoal.RightHand, ikData.rightHandRot);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikData.leftFootPosWeight);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, ikData.leftFootPos);

                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikData.leftFootRotWeight);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, ikData.leftFootRot);

                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikData.rightFootPosWeight);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, ikData.rightFootPos);

                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikData.rightFootRotWeight);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, ikData.rightFootRot);
            }
        }

        public void UpdateData(IKData newData)
        {
            ikData = newData;
            animator.Update(0);
        }
    }
}