using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Gamekit3D.GameCommands
{

    public class SetGameObjectActive : GameCommandHandler
    {
        public GameObject[] targets;
        public bool isEnabled = true;

        public override void PerformInteraction()
        {
			//         foreach(var g in targets)
			//{
			//             g.SetActive(isEnabled);
			//}

			//for (int g = 0; g < targets.Length; g++)
			//{
			//	targets[g].SetActive(isEnabled);
			//}

			StartCoroutine(DelayedEnable());
		}

		IEnumerator DelayedEnable()
		{
			int g = 0;
			while (g < targets.Length)
			{
				targets[g].SetActive(isEnabled);
				g++;
				yield return null;
			}
		}
	}
}
