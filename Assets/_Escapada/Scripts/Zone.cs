using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [Range(0f,10f)]
    [SerializeField] private float m_Speed;

    [Range(0f,10f)]
    [SerializeField] private float m_HitRate;
    [SerializeField] private float m_HitTimer;

	private void FixedUpdate()
	{
		MoveForward();
	}

	private void MoveForward() 
    {
        transform.position += Vector3.right * Time.deltaTime * m_Speed;
    }

	private bool CheckHitTimer()
	{
		m_HitTimer -= Time.deltaTime;

		if (m_HitTimer <= 0)
		{
			m_HitTimer = m_HitRate;

			return true;
		}

		return false;
	}

	private void Hit()
	{
		if (!CheckHitTimer())
		{
			return;
		}

		EventManager.Instance.HurtPlayer(1);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;

		if (layer == Layers.PLAYER)
		{
			Hit();
		}
	}
}
