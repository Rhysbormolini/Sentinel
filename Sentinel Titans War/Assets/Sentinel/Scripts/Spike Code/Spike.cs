using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	float m_StartHeight;
	bool m_Rising;
	bool Enabled;
	private void OnEnable()
	{
		m_StartHeight = this.transform.position.y;
	}

	public void Activate(float height, float speed)
	{
		Debug.Log("Spike " + name + " Activated");
		Enabled = true;
		m_Rising = true;
		while (Enabled == true)
		{
			if (this.transform.position.y < height && m_Rising)
			{
				this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
			}
			else if (this.transform.position.y > m_StartHeight && !m_Rising)
			{
				this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
			}

			if (this.transform.position.y >= height)
			{
				m_Rising = false;
			}
			else if (this.transform.position.y <= m_StartHeight)
			{
				Enabled = false;
			}
		}

	}
}
