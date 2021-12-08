/*
This is a example script to show how you can manipulate CinemaIK component through scripts.
It shows basic use of a CinemaIK methods.
*/

using UnityEngine;
using NorthLab.CinemaIKDemo;

public class APIExample : MonoBehaviour
{

    public CinemaIK cinemaIK;
    [Space]
    public Transform lookTarget;
    [Space]
    public Transform leftHandTarget;
    [Range(0, 1)]
    public float leftHandWeight;
    [Space]
    public Transform rightHandTarget;
    [Range(0, 1)]
    public float rightHandWeight;
    [Space]
    public Transform leftFootTarget;
    [Range(0, 1)]
    public float leftFootWeight;
    [Space]
    public Transform rightFootTarget;
    [Range(0, 1)]
    public float rightFootWeight;

    private void Update()
    {
        //If lookTarget is not null
        if (lookTarget)
        {
            cinemaIK.SetLookIK(lookTarget);
        }

        //If leftHandTarget is not null
        if (leftHandTarget)
        {
            cinemaIK.SetLeftHandIK(leftHandTarget, leftHandWeight, leftHandWeight);
        }
        //In some cases you need to send only position or rotation without any transforms, its possible too
        //Uncomment to see the result
        /*if (leftHandTarget)
        {
            cinemaIK.SetLeftHandIK(leftHandTarget.position, leftHandTarget.rotation, leftHandWeight);
        }*/

        //If rightHandTarget is not null
        if (rightHandTarget)
        {
            cinemaIK.SetRightHandIK(rightHandTarget, rightHandWeight, rightHandWeight);
        }

        //If leftFootTarget is not null
        if (leftFootTarget)
        {
            cinemaIK.SetLeftFootIK(leftFootTarget, leftFootWeight, leftFootWeight);
        }

        //If rightFootTarget is not null
        if (rightFootTarget)
        {
            cinemaIK.SetRightFootIK(rightFootTarget, rightFootWeight, rightFootWeight);
        }
    }

}