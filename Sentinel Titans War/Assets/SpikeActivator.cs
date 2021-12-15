using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeActivator : MonoBehaviour
{
	[SerializeField] SpikeController SpikeController;
	bool playerInArea = false;
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if (playerInArea)
			{
				SpikeController.StopSpikes();
				playerInArea = false;
			}
			else
			{
				SpikeController.StartSpikes();
				playerInArea = true;
			}
		}
	}
}
