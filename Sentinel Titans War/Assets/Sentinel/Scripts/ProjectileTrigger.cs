using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{

    public GameObject tornadoProjectile;    // this is a reference to your projectile prefab
    public Transform tornadoSpawnTransform; // this is a reference to the transform where the prefab will spawn

    [SerializeField]
    float tornadoAbilityCoolDownTime;
    float tornadoAbilityCoolDown;

    public GameObject lightningBallProjectile;    // this is a reference to your projectile prefab
    public Transform lightningBallSpawnTransform; // this is a reference to the transform where the prefab will spawn

    [SerializeField]
    float lightningBallAbilityCoolDownTime;
    float lightningBallAbilityCoolDown;

    //public Animator m_Animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && tornadoAbilityCoolDown <= 0)
        {
            //m_Animator.Play("TornadoAnim");
            Instantiate(tornadoProjectile, tornadoSpawnTransform.position, tornadoSpawnTransform.rotation);
            tornadoAbilityCoolDown = tornadoAbilityCoolDownTime;
        }

        if (tornadoAbilityCoolDown > 0)
        {
            tornadoAbilityCoolDown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E) && lightningBallAbilityCoolDown <= 0)
        {
            Instantiate(lightningBallProjectile, lightningBallSpawnTransform.position, lightningBallSpawnTransform.rotation);
            lightningBallAbilityCoolDown = lightningBallAbilityCoolDownTime;
        }

        if (lightningBallAbilityCoolDown > 0)
        {
            lightningBallAbilityCoolDown -= Time.deltaTime;
        }
    }
}
