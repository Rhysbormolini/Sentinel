using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Gamekit3D
{
    public class ProjectileTrigger : MonoBehaviour
    {
        public Animator m_Animator;

        [Header("Tornado")]
        public static bool tornadoUnlocked;
        public GameObject tornadoProjectile;    // this is a reference to your projectile prefab
        public Transform tornadoSpawnTransform; // this is a reference to the transform where the prefab will spawn
        [SerializeField] float tornadoAbilityCoolDownTime;
        [SerializeField] float tornadoAbilityCoolDown;

        [Header("Chain Lightning")]
        public static bool ballUnlocked;
        public GameObject lightningBallProjectile;    // this is a reference to your projectile prefab
        public Transform lightningBallSpawnTransform; // this is a reference to the transform where the prefab will spawn
        [SerializeField] float lightningBallAbilityCoolDownTime;
        [SerializeField] float lightningBallAbilityCoolDown;

        [Header("Lightning Storm")]
        public static bool stormUnlocked;
        public GameObject lightningStormProjectile;    // this is a reference to your projectile prefab
        public Transform lightningStormSpawnTransform; // this is a reference to the transform where the prefab will spawn
        [SerializeField] float lightningStormAbilityCoolDownTime;
        [SerializeField] float lightningStormAbilityCoolDown;
        public LayerMask layerMask;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && tornadoAbilityCoolDown <= 0 && tornadoUnlocked == true)
            {
                m_Animator.Play("TornadoAnim");
                Instantiate(tornadoProjectile, tornadoSpawnTransform.position, Camera.main.transform.rotation);
                tornadoAbilityCoolDown = tornadoAbilityCoolDownTime;
            }

            if (tornadoAbilityCoolDown > 0)
            {
                tornadoAbilityCoolDown -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.E) && lightningBallAbilityCoolDown <= 0 && ballUnlocked == true)
            {
                m_Animator.Play("ZapZapAnim");
                Instantiate(lightningBallProjectile, lightningBallSpawnTransform.position, Camera.main.transform.rotation);
                lightningBallAbilityCoolDown = lightningBallAbilityCoolDownTime;
            }

            if (lightningBallAbilityCoolDown > 0)
            {
                lightningBallAbilityCoolDown -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.R) && lightningStormAbilityCoolDown <= 0 && stormUnlocked == true)
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
