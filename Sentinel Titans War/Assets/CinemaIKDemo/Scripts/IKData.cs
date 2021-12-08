using UnityEngine;

namespace NorthLab.CinemaIKDemo
{
    public class IKData
    {

        public float weight;
        public Vector3 lookAtPos;

        public float leftHandPosWeight;
        public Vector3 leftHandPos;
        public float leftHandRotWeight;
        public Quaternion leftHandRot;

        public float rightHandPosWeight;
        public Vector3 rightHandPos;
        public float rightHandRotWeight;
        public Quaternion rightHandRot;

        public float leftFootPosWeight;
        public Vector3 leftFootPos;
        public float leftFootRotWeight;
        public Quaternion leftFootRot;

        public float rightFootPosWeight;
        public Vector3 rightFootPos;
        public float rightFootRotWeight;
        public Quaternion rightFootRot;

    }
}