using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{

    public float MaxAngle = 100f;
    public Vector3 LookTarget;
    public float Weight;
    public float BodyWeight;
    public float HeadWeight;

    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetLookAtWeight(Weight, BodyWeight, HeadWeight);

        //float deltaAngle = Vector3.SignedAngle(transform.forward, LookTarget - transform.position, transform.up);
        //if (Mathf.Abs(deltaAngle) > MaxAngle)
        //{
        //    transform.Rotate(new Vector3(0, deltaAngle, 0), Space.Self);
        //}
        _animator.SetLookAtPosition(LookTarget);
    }

}
