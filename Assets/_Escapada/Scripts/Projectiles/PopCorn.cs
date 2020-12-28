using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCorn : MonoBehaviour
{
	[Header("Values")]
	[Range(0f, 30f)]
	[SerializeField] private float m_Range;
	[Range(0f, 30f)]
	[SerializeField] private float m_Speed;

	[Header("Components")]
	[SerializeField] private Rigidbody2D m_Rigidbody2D;

	private float m_VanishPos;

	private void OnEnable()
	{
		Init();
	}

	private void Init()
	{
		m_VanishPos = transform.localPosition.x + m_Range;
	}

	private void FixedUpdate()
	{
		if (RangeCheck())
		{
			return;
		}

		Vanish();
	}

	private bool RangeCheck()
	{
		return transform.localPosition.x < m_VanishPos;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;

		if (layer == Layers.PLAYER)
		{
			EventManager.Instance.HurtPlayer?.Invoke(1);
		}

		Debug.Log(collision.name + "nanda");
		Vanish();
	}

	private void Vanish()
	{

		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.right * m_Range);
	}
}
