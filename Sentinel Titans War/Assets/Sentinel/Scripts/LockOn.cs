using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

namespace Gamekit3D
{
    public class LockOn : MonoBehaviour
    {
        public float playerVisionRadius = 20f;
        public List<GameObject> targets = new List<GameObject> { };
        public Transform nearestEnemy;
		public Transform currentEnemy;
		bool isLockingOn = false;
		[SerializeField] LayerMask enemyLayer;


		private void FixedUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				// swap lock on to closet target within range
				isLockingOn = true;
				HandleLockOn();
			}

			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				// swap lock on to next closest target within range
				SwapTargets();
			}
		}

		public void HandleLockOn()
		{
			RaycastHit hit;
			targets = Physics.SphereCast(transform.position, playerVisionRadius, transform.forward, out hit);
		
			
			
			nearestEnemy = targets
			.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)
			.FirstOrDefault();
		}

		public void SwapTargets()
		{
			if (isLockingOn == true)
			{
				
			}	
		}

		public void CameraLock()
		{

		}

		private void OnDrawGizmos()
		{
			// draw radius gizmo
		}
	}
}
