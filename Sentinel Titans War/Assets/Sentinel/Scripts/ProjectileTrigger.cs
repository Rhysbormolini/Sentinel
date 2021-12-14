using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

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
        bool tornadoCooldown = false;
        public Image tornadoImage;

        [Header("Chain Lightning")]
        public GameObject lightningBallProjectile;    // this is a reference to your projectile prefab
        public Transform lightningBallSpawnTransform; // this is a reference to the transform where the prefab will spawn
        [SerializeField] float lightningBallAbilityCoolDownTime;
        [SerializeField] float lightningBallAbilityCoolDown;
        bool lightningCooldown = false;
        public Image lightningImage;

        [Header("Lightning Storm")]
        public GameObject lightningStormProjectile;    // this is a reference to your projectile prefab
        public Transform lightningStormSpawnTransform; // this is a reference to the transform where the prefab will spawn
        [SerializeField] float lightningStormAbilityCoolDownTime;
        [SerializeField] float lightningStormAbilityCoolDown;
        bool stormCooldown = false;
        public Image stormImage;


        private void Start()
		{
            tornadoImage.fillAmount = 1;
            lightningImage.fillAmount = 1;
            stormImage.fillAmount = 1;
		}

        private void Update()
        {
			#region
			if (tornadoUnlocked && tornadoCooldown == false)
			{
                tornadoImage.fillAmount = 0;
            }
            

            if (Input.GetKeyDown(KeyCode.Q) && tornadoAbilityCoolDown <= 0 && tornadoUnlocked == true)
            {
                m_Animator.Play("TornadoAnim");
                Instantiate(tornadoProjectile, tornadoSpawnTransform.position, Camera.main.transform.rotation);
                tornadoAbilityCoolDown = tornadoAbilityCoolDownTime;
                tornadoCooldown = true;
                tornadoImage.fillAmount = 1;
            }
            else if (Input.GetButton("Tornado") && tornadoAbilityCoolDown <= 0 && tornadoUnlocked == true)
            {
                m_Animator.Play("TornadoAnim");
                Instantiate(tornadoProjectile, tornadoSpawnTransform.position, Camera.main.transform.rotation);
                tornadoAbilityCoolDown = tornadoAbilityCoolDownTime;
                tornadoCooldown = true;
                tornadoImage.fillAmount = 1;
            }


            if (tornadoAbilityCoolDown > 0)
            {
                tornadoAbilityCoolDown -= Time.deltaTime;  
            }

            if (tornadoCooldown)
            {
                tornadoImage.fillAmount -= 1 / tornadoAbilityCoolDownTime * Time.deltaTime;
            }
			#endregion

			#region Lightning Orb
			if (ballUnlocked && lightningCooldown == false)
            {
                lightningImage.fillAmount = 0;
            }

            if (Input.GetKeyDown(KeyCode.E) && lightningBallAbilityCoolDown <= 0 && ballUnlocked == true)
            {
                m_Animator.Play("ZapZapAnim");
                Instantiate(lightningBallProjectile, lightningBallSpawnTransform.position, Camera.main.transform.rotation);
                lightningBallAbilityCoolDown = lightningBallAbilityCoolDownTime;
                lightningCooldown = true;
                lightningImage.fillAmount = 1;

            }
            else if (Input.GetButton("Ball") && lightningBallAbilityCoolDown <= 0 && ballUnlocked == true)
            {
                m_Animator.Play("ZapZapAnim");
                Instantiate(lightningBallProjectile, lightningBallSpawnTransform.position, Camera.main.transform.rotation);
                lightningBallAbilityCoolDown = lightningBallAbilityCoolDownTime;
                lightningCooldown = true;
                lightningImage.fillAmount = 1;
            }


            if (lightningBallAbilityCoolDown > 0)
            {
                lightningBallAbilityCoolDown -= Time.deltaTime;
            }

            if (lightningCooldown)
			{
                lightningImage.fillAmount -= 1 / lightningBallAbilityCoolDownTime * Time.deltaTime;
            }
			#endregion

			if (stormUnlocked && stormCooldown == false)
            {
                stormImage.fillAmount = 0;
            }
           

            if (Input.GetKeyDown(KeyCode.R) && lightningStormAbilityCoolDown <= 0 && stormUnlocked == true)
            {
                m_Animator.Play("StormAnim");
                Instantiate(lightningStormProjectile, lightningStormSpawnTransform.position, transform.rotation);
                lightningStormAbilityCoolDown = lightningStormAbilityCoolDownTime;
                stormCooldown = true;
                stormImage.fillAmount = 1;
            }
            else if (Input.GetButton("Storm") && lightningStormAbilityCoolDown <= 0 && stormUnlocked == true)
            {
                m_Animator.Play("StormAnim");
                Instantiate(lightningStormProjectile, lightningStormSpawnTransform.position, transform.rotation);
                lightningStormAbilityCoolDown = lightningStormAbilityCoolDownTime;
                stormCooldown = true;
                stormImage.fillAmount = 1;
            }

            if (lightningStormAbilityCoolDown > 0)
            {
                lightningStormAbilityCoolDown -= Time.deltaTime;
                
            }

            if (stormCooldown)
            {
                stormImage.fillAmount -= 1 / lightningStormAbilityCoolDownTime * Time.deltaTime;
            }
        }
	}
}
