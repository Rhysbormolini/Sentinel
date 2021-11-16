using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
	public class Ability : MonoBehaviour
	{
		public Transform player;
		public GameObject pickUpEffect;
		public Animator animator;

		void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				PickUp();
			}
		}

		void PickUp()
		{
			// spawn a cool effect 
			Instantiate(pickUpEffect, player.position, player.rotation);
			// Play Power Up Animation
			animator.Play("PowerUp");

			// apply efect to player


			//Destroy pickup
			Destroy(gameObject);
		}
	}
}
