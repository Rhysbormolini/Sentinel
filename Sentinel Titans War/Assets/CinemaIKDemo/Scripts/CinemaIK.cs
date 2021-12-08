using System.Collections.Generic;
using UnityEngine;

namespace NorthLab.CinemaIKDemo
{
    [ExecuteInEditMode]
    public class CinemaIK : MonoBehaviour
    {

        [SerializeField]
        private Animator animator = null;
        public Animator Animator => animator;
        [SerializeField, Tooltip("Switch between game mode and timeline mode. Game mode recommended if you use CinemaIK without any timelines or animators through the scripts.")]
        private bool gameMode = false;
        /// <summary>
        /// Switch between game mode and timeline mode. Game mode recommended if you use CinemaIK without any timelines or animators through the scripts.
        /// </summary>
        public bool GameMode
        {
            get { return gameMode; }
            set { gameMode = value; }
        }

        //Look at
        [SerializeField, Tooltip("Object that we gonna look at")]
        private Transform lookAt = null;
        public Transform LookAt => lookAt;
        [SerializeField, Range(0, 1), Tooltip("The global weight of the LookAt, multiplier for other parameters.")]
        private float weight = 1;

        [SerializeField]
        private Vector3 lookAtPos = Vector3.zero;
        public Vector3 LookAtPos
        {
            get { return lookAtPos; }
            set { lookAtPos = value; }
        }

        //ik
        //lefthand
        [SerializeField, Range(0, 1), Tooltip("Sets the translative weight of an IK hint (0 = at the original animation before IK, 1 = at the hint)")]
        private float leftHandPositionWeight = 0;
        [SerializeField, Range(0, 1), Tooltip("Sets the translative weight of an IK hint (0 = at the original animation before IK, 1 = at the hint)")]
        private float leftHandRotationWeight = 0;
        [SerializeField, Tooltip("Left hand IK target")]
        private Transform leftHandIKTarget = null;
        public Transform LeftHandIKTarget => leftHandIKTarget;

        [SerializeField]
        private Vector3 leftHandIKPos = Vector3.zero;
        public Vector3 LeftHandIKPos
        {
            get { return leftHandIKPos; }
            set { leftHandIKPos = value; }
        }
        [SerializeField]
        private Quaternion leftHandIKRot = Quaternion.identity;
        public Quaternion LeftHandIKRot
        {
            get { return leftHandIKRot; }
            set { leftHandIKRot = value; }
        }

        //righthand
        [SerializeField, Range(0, 1), Tooltip("Sets the translative weight of an IK hint (0 = at the original animation before IK, 1 = at the hint)")]
        private float rightHandPositionWeight = 0;
        [SerializeField, Range(0, 1), Tooltip("Sets the translative weight of an IK hint (0 = at the original animation before IK, 1 = at the hint)")]
        private float rightHandRotationWeight = 0;
        [SerializeField, Tooltip("Right hand IK target")]
        private Transform rightHandIKTarget = null;
        public Transform RightHandIKTarget => rightHandIKTarget;

        [SerializeField]
        private Vector3 rightHandIKPos = Vector3.zero;
        public Vector3 RightHandIKPos
        {
            get { return rightHandIKPos; }
            set { rightHandIKPos = value; }
        }
        [SerializeField]
        private Quaternion rightHandIKRot = Quaternion.identity;
        public Quaternion RightHandIKRot
        {
            get { return rightHandIKRot; }
            set { rightHandIKRot = value; }
        }

        //leftfoot
        [SerializeField, Range(0, 1), Tooltip("Sets the translative weight of an IK hint (0 = at the original animation before IK, 1 = at the hint)")]
        private float leftFootPositionWeight = 0;
        [SerializeField, Range(0, 1), Tooltip("Sets the translative weight of an IK hint (0 = at the original animation before IK, 1 = at the hint)")]
        private float leftFootRotationWeight = 0;
        [SerializeField, Tooltip("Left foot IK target")]
        private Transform leftFootIKTarget = null;
        public Transform LeftFootIKTarget => leftFootIKTarget;

        [SerializeField]
        private Vector3 leftFootIKPos = Vector3.zero;
        public Vector3 LeftFootIKPos
        {
            get { return leftFootIKPos; }
            set { leftFootIKPos = value; }
        }
        [SerializeField]
        private Quaternion leftFootIKRot = Quaternion.identity;
        public Quaternion LeftFootIKRot
        {
            get { return leftFootIKRot; }
            set { leftFootIKRot = value; }
        }

        //rightfoot
        [SerializeField, Range(0, 1), Tooltip("Sets the translative weight of an IK hint (0 = at the original animation before IK, 1 = at the hint)")]
        private float rightFootPositionWeight = 0;
        [SerializeField, Range(0, 1), Tooltip("Sets the translative weight of an IK hint (0 = at the original animation before IK, 1 = at the hint)")]
        private float rightFootRotationWeight = 0;
        [SerializeField, Tooltip("Right foot IK target")]
        private Transform rightFootIKTarget = null;
        public Transform RightFootIKTarget => rightFootIKTarget;

        [SerializeField]
        private Vector3 rightFootIKPos = Vector3.zero;
        public Vector3 RightFootIKPos
        {
            get { return rightFootIKPos; }
            set { rightFootIKPos = value; }
        }
        [SerializeField]
        private Quaternion rightFootIKRot = Quaternion.identity;
        public Quaternion RightFootIKRot
        {
            get { return rightFootIKRot; }
            set { rightFootIKRot = value; }
        }

        [SerializeField]
        private bool showGizmos = true;
        [SerializeField]
        private bool showBoneLines = false;

        //foldOuts
        public bool LookFoldOut { get; set; }
        public bool IksFoldOut { get; set; }
        public bool LeftHandFoldOut { get; set; }
        public bool RightHandFoldOut { get; set; }
        public bool LeftFootFoldOut { get; set; }
        public bool RightFootFoldOut { get; set; }

        private CinemaIKAnchor anchor;
        private IKData ikData;
        private Animator oldAnimator;

        private void OnEnable()
        {
            Reload();
        }

        private void OnDestroy()
        {
            Reload();

            if (!anchor)
                return;
            //destroy animator anchor
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () =>
            {
                DestroyImmediate(anchor);
            };
#else
        Destroy(anchor);
#endif
        }

        private void OnDisable()
        {
            Reload();
        }

        private void Reset()
        {
            Reload();

            //destroy animator anchor
            if (anchor)
                DestroyImmediate(anchor);
        }

        private void Update()
        {
            //if animator is assigned and its have an anchor then update IKs
            if (animator)
            {
                if (animator != oldAnimator)
                {
                    if (oldAnimator)
                    {
                        DestroyImmediate(anchor);
                    }
                    oldAnimator = animator;

                    if (!animator.gameObject.GetComponent<CinemaIKAnchor>())
                    {
                        anchor = animator.gameObject.AddComponent<CinemaIKAnchor>();
                    }
                    else
                    {
                        anchor = animator.gameObject.GetComponent<CinemaIKAnchor>();
                    }
                }

                if (lookAt && !gameMode)
                    lookAt.transform.position = lookAtPos;

                UpdateIK();
            }

            //sync the IK target poses with properties that we animate
            if (animator && !gameMode)
            {
                if (leftHandIKTarget)
                {
                    leftHandIKTarget.position = leftHandIKPos;
                    leftHandIKTarget.rotation = leftHandIKRot;
                }
                if (rightHandIKTarget)
                {
                    rightHandIKTarget.position = rightHandIKPos;
                    rightHandIKTarget.rotation = rightHandIKRot;
                }
                if (leftFootIKTarget)
                {
                    leftFootIKTarget.position = leftFootIKPos;
                    leftFootIKTarget.rotation = leftFootIKRot;
                }
                if (rightFootIKTarget)
                {
                    rightFootIKTarget.position = rightFootIKPos;
                    rightFootIKTarget.rotation = rightFootIKRot;
                }
            }
        }

        //main IK update function
        private void UpdateIK()
        {
            //if anchor is available
            if (anchor)
            {
                //make new IKData to send to the anchor
                ikData = new IKData();

                //populating ikData with data
                if (animator)
                {
                    if (lookAt)
                    {
                        ikData.weight = weight;
                        ikData.lookAtPos = lookAtPos;
                    }

                    if (leftHandIKTarget)
                    {
                        ikData.leftHandPosWeight = leftHandPositionWeight;
                        ikData.leftHandPos = leftHandIKPos;

                        ikData.leftHandRotWeight = leftHandRotationWeight;
                        ikData.leftHandRot = leftHandIKRot;
                    }
                    if (rightHandIKTarget)
                    {
                        ikData.rightHandPosWeight = rightHandPositionWeight;
                        ikData.rightHandPos = rightHandIKPos;

                        ikData.rightHandRotWeight = rightHandRotationWeight;
                        ikData.rightHandRot = rightHandIKRot;
                    }
                    if (leftFootIKTarget)
                    {
                        ikData.leftFootPosWeight = leftFootPositionWeight;
                        ikData.leftFootPos = leftFootIKPos;

                        ikData.leftFootRotWeight = leftFootRotationWeight;
                        ikData.leftFootRot = leftFootIKRot;
                    }
                    if (rightFootIKTarget)
                    {
                        ikData.rightFootPosWeight = rightFootPositionWeight;
                        ikData.rightFootPos = rightFootIKPos;

                        ikData.rightFootRotWeight = rightFootRotationWeight;
                        ikData.rightFootRot = rightFootIKRot;
                    }
                }

                //sending ikData to anchor
                anchor.UpdateData(ikData);
            }
        }

        private void OnDrawGizmosSelected()
        {
            //if showGizmos is false and animator is null then skip
            if (!showGizmos)
                return;

            if (!animator)
                return;

            //if lookAt draw eye icon
            if (lookAt)
                Gizmos.DrawIcon(lookAtPos, "blendKeySelected", false);

            //IK gizmos
            if (leftHandIKTarget)
            {
                Gizmos.DrawIcon(leftHandIKPos, "d_ViewToolMove On", false);
                if (showBoneLines)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.LeftUpperArm).position, animator.GetBoneTransform(HumanBodyBones.LeftLowerArm).transform.position);
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.LeftLowerArm).position, animator.GetBoneTransform(HumanBodyBones.LeftHand).transform.position);
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.LeftHand).position, animator.GetBoneTransform(HumanBodyBones.LeftMiddleIntermediate).transform.position);
                }
            }
            if (rightHandIKTarget)
            {
                Gizmos.DrawIcon(rightHandIKPos, "d_ViewToolMove On", false);
                if (showBoneLines)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.RightUpperArm).position, animator.GetBoneTransform(HumanBodyBones.RightLowerArm).transform.position);
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.RightLowerArm).position, animator.GetBoneTransform(HumanBodyBones.RightHand).transform.position);
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.RightHand).position, animator.GetBoneTransform(HumanBodyBones.RightMiddleIntermediate).transform.position);
                }
            }
            if (leftFootIKTarget)
            {
                Gizmos.DrawIcon(leftFootIKPos, "d_ViewToolMove On", false);
                if (showBoneLines)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).position, animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).transform.position);
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).position, animator.GetBoneTransform(HumanBodyBones.LeftFoot).transform.position);
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.LeftFoot).position, animator.GetBoneTransform(HumanBodyBones.LeftToes).transform.position);
                }
            }
            if (rightFootIKTarget)
            {
                Gizmos.DrawIcon(rightFootIKPos, "d_ViewToolMove On", false);
                if (showBoneLines)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.RightUpperLeg).position, animator.GetBoneTransform(HumanBodyBones.RightLowerLeg).transform.position);
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.RightLowerLeg).position, animator.GetBoneTransform(HumanBodyBones.RightFoot).transform.position);
                    Gizmos.DrawLine(animator.GetBoneTransform(HumanBodyBones.RightFoot).position, animator.GetBoneTransform(HumanBodyBones.RightToes).transform.position);
                }
            }
        }

        //reloading component
        private void Reload()
        {
            //set all IK weights to zero
            if (animator)
            {
                if (lookAt)
                {
                    animator.SetLookAtWeight(0, 0, 0, 0, 0);
                }

                if (leftHandIKTarget)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                }
                if (rightHandIKTarget)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                }
                if (leftFootIKTarget)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
                }
                if (rightFootIKTarget)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
                }

                UpdateIK();
            }
        }

        #region Functions
        /// <summary>
        /// Set look target
        /// </summary>
        /// <param name="target">Target transform</param>
        /// <param name="weight">Look weight from 0 to 1</param>
        public void SetLookIK(Transform target, float weight = 1)
        {
            lookAt = target;
            lookAtPos = target.position;
            this.weight = weight;
        }

        /// <summary>
        /// Set look target
        /// </summary>
        /// <param name="target">Target position</param>
        /// <param name="weight">Look weight from 0 to 1</param>
        public void SetLookIK(Vector3 target, float weight = 1)
        {
            lookAt = transform;
            lookAtPos = target;
            this.weight = weight;
        }

        /// <summary>
        /// Set left hand IK target
        /// </summary>
        /// <param name="target">Target transform</param>
        /// <param name="leftHandPositionWeight">Position weight from 0 to 1</param>
        /// <param name="leftHandRotationWeight">Rotation weight from 0 to 1</param>
        public void SetLeftHandIK(Transform target, float leftHandPositionWeight = 1, float leftHandRotationWeight = 1)
        {
            leftHandIKTarget = target;
            leftHandIKPos = target.position;
            leftHandIKRot = target.rotation;
            this.leftHandPositionWeight = leftHandPositionWeight;
            this.leftHandRotationWeight = leftHandRotationWeight;
        }

        /// <summary>
        /// Set left hand IK target
        /// </summary>
        /// <param name="pos">Target position</param>
        /// <param name="rot">Target rotation</param>
        /// <param name="leftHandPositionWeight">Position weight from 0 to 1</param>
        /// <param name="leftHandRotationWeight">Rotation weight from 0 to 1</param>
        public void SetLeftHandIK(Vector3 pos, Quaternion rot, float leftHandPositionWeight = 1, float leftHandRotationWeight = 1)
        {
            leftHandIKTarget = transform;
            leftHandIKPos = pos;
            leftHandIKRot = rot;
            this.leftHandPositionWeight = leftHandPositionWeight;
            this.leftHandRotationWeight = leftHandRotationWeight;
        }

        /// <summary>
        /// Set right hand IK target
        /// </summary>
        /// <param name="target">Target transform</param>
        /// <param name="leftHandPositionWeight">Position weight from 0 to 1</param>
        /// <param name="leftHandRotationWeight">Rotation weight from 0 to 1</param>
        public void SetRightHandIK(Transform target, float rightHandPositionWeight = 1, float rightHandRotationWeight = 1)
        {
            rightHandIKTarget = target;
            rightHandIKPos = target.position;
            rightHandIKRot = target.rotation;
            this.rightHandPositionWeight = rightHandPositionWeight;
            this.rightHandRotationWeight = rightHandRotationWeight;
        }

        /// <summary>
        /// Set right hand IK target
        /// </summary>
        /// <param name="pos">Target position</param>
        /// <param name="rot">Target rotation</param>
        /// <param name="leftHandPositionWeight">Position weight from 0 to 1</param>
        /// <param name="leftHandRotationWeight">Rotation weight from 0 to 1</param>
        public void SetRightHandIK(Vector3 pos, Quaternion rot, float rightHandPositionWeight = 1, float rightHandRotationWeight = 1)
        {
            rightHandIKTarget = transform;
            rightHandIKPos = pos;
            rightHandIKRot = rot;
            this.rightHandPositionWeight = rightHandPositionWeight;
            this.rightHandRotationWeight = rightHandRotationWeight;
        }

        /// <summary>
        /// Set left foot IK target
        /// </summary>
        /// <param name="target">Target transform</param>
        /// <param name="leftHandPositionWeight">Position weight from 0 to 1</param>
        /// <param name="leftHandRotationWeight">Rotation weight from 0 to 1</param>
        public void SetLeftFootIK(Transform target, float leftFootPositionWeight = 1, float leftFootRotationWeight = 1)
        {
            leftFootIKTarget = target;
            leftFootIKPos = target.position;
            leftFootIKRot = target.rotation;
            this.leftFootPositionWeight = leftFootPositionWeight;
            this.leftFootRotationWeight = leftFootRotationWeight;
        }

        /// <summary>
        /// Set left foot IK target
        /// </summary>
        /// <param name="pos">Target position</param>
        /// <param name="rot">Target rotation</param>
        /// <param name="leftHandPositionWeight">Position weight from 0 to 1</param>
        /// <param name="leftHandRotationWeight">Rotation weight from 0 to 1</param>
        public void SetLeftFootIK(Vector3 pos, Quaternion rot, float leftFootPositionWeight = 1, float leftFootRotationWeight = 1)
        {
            leftFootIKTarget = transform;
            leftFootIKPos = pos;
            leftFootIKRot = rot;
            this.leftFootPositionWeight = leftFootPositionWeight;
            this.leftFootRotationWeight = leftFootRotationWeight;
        }

        /// <summary>
        /// Set right foot IK target
        /// </summary>
        /// <param name="target">Target transform</param>
        /// <param name="leftHandPositionWeight">Position weight from 0 to 1</param>
        /// <param name="leftHandRotationWeight">Rotation weight from 0 to 1</param>
        public void SetRightFootIK(Transform target, float rightFootPositionWeight = 1, float rightFootRotationWeight = 1)
        {
            rightFootIKTarget = target;
            rightFootIKPos = target.position;
            rightFootIKRot = target.rotation;
            this.rightFootPositionWeight = rightFootPositionWeight;
            this.rightFootRotationWeight = rightFootRotationWeight;
        }

        /// <summary>
        /// Set right foot IK target
        /// </summary>
        /// <param name="pos">Target position</param>
        /// <param name="rot">Target rotation</param>
        /// <param name="leftHandPositionWeight">Position weight from 0 to 1</param>
        /// <param name="leftHandRotationWeight">Rotation weight from 0 to 1</param>
        public void SetRightFootIK(Vector3 pos, Quaternion rot, float rightFootPositionWeight = 1, float rightFootRotationWeight = 1)
        {
            rightFootIKTarget = transform;
            rightFootIKPos = pos;
            rightFootIKRot = rot;
            this.rightFootPositionWeight = rightFootPositionWeight;
            this.rightFootRotationWeight = rightFootRotationWeight;
        }
        #endregion
    }
}