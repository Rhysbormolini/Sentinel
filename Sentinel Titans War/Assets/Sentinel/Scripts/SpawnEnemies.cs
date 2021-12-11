using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    [System.Serializable]
    public struct Wave
    {
        // name each way to keep inspector organised
        public string waveName;
        // Enemies in each wave
        public GameObject[] waveEnemies;
        public bool activate;
    }
    public class SpawnEnemies : MonoBehaviour
    {
        public List<Wave> waves = new List<Wave>();

        public void SpawnEnemiesWaveOne()
		{
            foreach (GameObject g in waves[0].waveEnemies)
            {
                g.SetActive(waves[0].activate);
            }
        }

        public void SpawnEnemiesWaveTwo()
        {
            foreach (GameObject g in waves[1].waveEnemies)
            {
                g.SetActive(waves[2].activate);
            }
        }

        public void SpawnEnemiesWaveThree()
        {
            foreach (GameObject g in waves[2].waveEnemies)
            {
                g.SetActive(waves[2].activate);
            }
        }
    }
}

