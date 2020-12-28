using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
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
		ShootSelf();
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

	private void ShootSelf()
	{
		if (Player.IsLookingLeft)
		{
			m_Rigidbody2D.AddForce(Vector2.left * m_Speed, ForceMode2D.Impulse);
		}
		else
		{
			m_Rigidbody2D.AddForce(Vector2.right * m_Speed, ForceMode2D.Impulse);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log(collision.gameObject.name);
		Vanish();
	}

	protected abstract void Vanish();

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.right * m_Range);
	}
}
