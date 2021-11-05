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
        public Transform nearestEnemy;
		public Transform currentEnemy;
		public bool isLockingOn = false;
		[SerializeField] LayerMask enemyLayer;
		CameraSettings cam;

		private void Awake()
		{
			cam = GetComponent<CameraSettings>();
			currentEnemy = null;
		}

		private void Update()
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
			RaycastHit[] targets = Physics.SphereCastAll(transform.position, playerVisionRadius, transform.forward);
			float minDistance = Mathf.Infinity;
			Transform closestEnemy = null;

			for (int i = 0; i > targets.Length; i++)
			{
				float currDistance = (targets[i].transform.position - transform.position).magnitude;

				if (currDistance < minDistance)
				{
					minDistance = currDistance;
					closestEnemy = targets[i].transform;
					Debug.Log("Closest Enemy is: " + closestEnemy.name);
					currentEnemy = closestEnemy;
				}
			}

			// shoot out raycast in a sphere to find enemies within radius
			// assisn them to the list
			// sort through to find the closest 
			// assign and lock the camera to the position 
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
