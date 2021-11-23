using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Gamekit3D
{
    public class ProjectileTrigger : MonoBehaviour
    {
        public static bool stormUnlocked;
        public static bool ballUnlocked;
        public static bool tornadoUnlocked;
        public Animator m_Animator;


        [Header("Tornado")]
        public GameObject tornadoProjectile;    // this is a reference to your projectile prefab
        public Transform tornadoSpawnTransform; // this is a reference to the transform where the prefab will spawn
        [SerializeField] float tornadoAbilityCoolDownTime;
        [SerializeField] float tornadoAbilityCoolDown;

        [Header("Chain Lightning")]
        public GameObject lightningBallProjectile;    // this is a reference to your projectile prefab
        public Transform lightningBallSpawnTransform; // this is a reference to the transform where the prefab will spawn
        [SerializeField] float lightningBallAbilityCoolDownTime;
        [SerializeField] float lightningBallAbilityCoolDown;

        [Header("Lightning Storm")]
        public GameObject lightningStormProjectile;    // this is a reference to your projectile prefab
        public Transform lightningStormSpawnTransform; // this is a reference to the transform where the prefab will spawn
        [SerializeField] float lightningStormAbilityCoolDownTime;
        [SerializeField] float lightningStormAbilityCoolDown;
        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetButton("Tornado")  && tornadoAbilityCoolDown <= 0 && tornadoUnlocked == true)
            {
                m_Animator.Play("TornadoAnim");
                Instantiate(tornadoProjectile, tornadoSpawnTransform.position, Camera.main.transform.rotation);
                tornadoAbilityCoolDown = tornadoAbilityCoolDownTime;
            }

            if (tornadoAbilityCoolDown > 0)
            {
                tornadoAbilityCoolDown -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetButton("Ball") && lightningBallAbilityCoolDown <= 0 && ballUnlocked == true)
            {
                m_Animator.Play("ZapZapAnim");
                Instantiate(lightningBallProjectile, lightningBallSpawnTransform.position, Camera.main.transform.rotation);
                lightningBallAbilityCoolDown = lightningBallAbilityCoolDownTime;
            }

            if (lightningBallAbilityCoolDown > 0)
            {
                lightningBallAbilityCoolDown -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.R) || Input.GetButton("Storm") && lightningStormAbilityCoolDown <= 0 && stormUnlocked == true)
            {
                m_Animator.Play("StormAnim");
                Instantiate(lightningStormProjectile, lightningStormSpawnTransform.position, transform.rotation);
                lightningStormAbilityCoolDown = lightningStormAbilityCoolDownTime;
                //StunEnemies();

            }

            if (lightningStormAbilityCoolDown > 0)
            {
                lightningStormAbilityCoolDown -= Time.deltaTime;
            }
        }

  //      private void StunEnemies()
		//{
  //          float radius = lightningStormProjectile.GetComponent<SphereCollider>().radius;

  //          Collider[] targets = Physics.OverlapSphere(tornadoSpawnTransform.position, radius * 10, layerMask);

  //          foreach (Collider collider in targets)
		//	{
  //              collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		//	}
		//}
	}
}
