using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isStorm = false;
    public float m_Speed = 10f;   // this is the projectile's speed
    public float m_Lifespan = 3f; // this is the projectile's lifespan (in seconds)

    private void FixedUpdate()
    {
        if (isStorm == false)
		{
            transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);
            Destroy(gameObject, m_Lifespan);
        }
        else
		{
            Destroy(gameObject, m_Lifespan);
        }  
    }
}
