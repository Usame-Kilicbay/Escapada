using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
	[Range(1f,10f)]
	[SerializeField] private float m_RollSpeed;

	private void Update()
	{
		Roll();
	}

	private void Roll()
	{
		transform.Rotate(Vector3.forward * m_RollSpeed);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;

		if (layer != Layers.PLAYER)
		{
			return;
		}

		EventManager.Instance.HurtPlayer?.Invoke(1);

		SelfDestruct();
	}

	private void SelfDestruct()
	{
		Destroy(gameObject);
	}
}
