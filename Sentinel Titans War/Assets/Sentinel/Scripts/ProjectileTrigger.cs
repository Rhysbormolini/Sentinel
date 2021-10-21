using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{

    public GameObject m_Projectile;    // this is a reference to your projectile prefab
    public Transform m_SpawnTransform; // this is a reference to the transform where the prefab will spawn

    [SerializeField]
    float abilityCoolDownTime;
    float abilityCoolDown;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && abilityCoolDown <= 0)
        {
            Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
            abilityCoolDown = abilityCoolDownTime;
        }

        if (abilityCoolDown > 0)
        {
            abilityCoolDown -= Time.deltaTime;
        }
    }
}
