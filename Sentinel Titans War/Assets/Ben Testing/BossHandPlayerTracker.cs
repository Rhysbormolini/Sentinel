using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Sentinals.Bosses
{
	public class BossHandPlayerTracker : MonoBehaviour
	{
		[SerializeField] float m_GroundHeight;
		[SerializeField] float m_SlamSpeed;
		[SerializeField, Tooltip("Time in Milliseconds")] int m_SlamDelay = 1000;
		bool m_Attacking = false;
		bool m_Slaming;
		float m_DefaultHeight;
		public bool Attacking { set { m_Attacking = value; } }

		private void OnEnable()
		{
			m_DefaultHeight = this.transform.position.y;
		}
		private void Update()
		{
			if (!m_Attacking)
			{
				this.transform.position = new Vector3(Gamekit3D.PlayerController.instance.transform.position.x, this.transform.position.y, Gamekit3D.PlayerController.instance.transform.position.z);
			}
			else
			{
				Slam();
				//TODO: Animator Fist Shake Code
			}
			if(!m_Slaming && this.transform.position.y >= m_DefaultHeight && m_Attacking)
			{
				//TODO: Smooth Transition Back to Over Player
				m_Attacking = false;
			}
		}

		Task Slam()
		{
			Task.Delay(m_SlamDelay);
			if (this.transform.position.y > m_GroundHeight && m_Slaming)
			{
				this.transform.position -= this.transform.up * m_SlamSpeed * Time.deltaTime;
			}
			else if (this.transform.position.y < m_DefaultHeight && !m_Slaming)
			{
				this.transform.position += this.transform.up * m_SlamSpeed * Time.deltaTime;
			}
			else
			{
				m_Slaming = !m_Slaming;
			}
			return null;
		}
	}
}