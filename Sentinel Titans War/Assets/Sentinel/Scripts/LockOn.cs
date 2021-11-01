using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    public class LockOn : MonoBehaviour
    {
        public float playerVisionRadius = 20f;
        public List<Damageable> targets = new List<Damageable> { };
        public Transform nearestEnemy;


		private void FixedUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				// swap lock on to closet target within range
			}

			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				// swap lock on to next closet target within range 
			}
		}
	}
}
