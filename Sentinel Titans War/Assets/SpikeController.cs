using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
	[SerializeField] List<GameObject> m_Spikes = new List<GameObject>();
	[SerializeField] float m_TimeBetweenSpikes;

	[SerializeField] float m_Speed;
	[SerializeField] float m_MaxHeight;

	int m_SpikeMax;
	GameObject m_Spike;
	bool m_Rising;
	bool m_SpikeMoving;
	int m_SpikeStartHeight;
	public void StartSpikes()
	{
		m_SpikeMax = m_Spikes.Count;
		m_Spike = m_Spikes[Random.Range(0, m_SpikeMax)];
		InvokeRepeating(nameof(Spikes), 0.1f, m_TimeBetweenSpikes);
		
	}

    void Spikes()
	{
		m_SpikeMoving = true;
		while (m_SpikeMoving)
		{
			if (m_Rising && m_Spike.transform.position.y < m_MaxHeight)
			{
				m_Spike.transform.position += new Vector3(0, m_Speed * Time.deltaTime, 0);
			}
			else if(!m_Rising && m_Spike.transform.position.y > m_SpikeStartHeight)
			{
				m_Spike.transform.position -= new Vector3(0, m_Speed * Time.deltaTime, 0);
			}
			if(m_Spike.transform.position.y >= m_MaxHeight)
			{
				m_Rising = false;
			}
			if(m_Spike.transform.position.y <= m_SpikeStartHeight && !m_Rising)
			{
				m_SpikeMoving = false;
				m_Spike = m_Spikes[Random.Range(0, m_SpikeMax)];
			}
		}
	}
}
