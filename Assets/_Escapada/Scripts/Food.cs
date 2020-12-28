using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;

		if (layer != Layers.PLAYER)
		{
			return;
		}

		EventManager.Instance.HealPlayer?.Invoke(1, SelfDestruct);
	}

	private void SelfDestruct()
	{
		Destroy(gameObject);
	}
}
