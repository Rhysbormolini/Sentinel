using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sentinals.Bosses
{
    public class BossAttackBehaviour : MonoBehaviour
    {
		[SerializeField] BossHandPlayerTracker playerTracker;

		[SerializeField] float timeOfNextAttack;

        [SerializeField] float timer = 0;

		private void OnEnable()
		{
			timeOfNextAttack = Random.Range(20f, 30f);
		}

		private void Update()
		{
			timer += Time.deltaTime;
			if(timer >= timeOfNextAttack)
			{
				playerTracker.Attacking = true;
				timeOfNextAttack = Random.Range(10f, 20f) + timer;
			}
		}
	}
}
