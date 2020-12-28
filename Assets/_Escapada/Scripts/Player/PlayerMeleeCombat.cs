using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
	[Header("Values")]
	[Range(1, 3)]
	[SerializeField] private int m_Damage;
	[Range(0, 3)]
	[SerializeField] private float m_AttackSpeed;
	[Range(0, 3)]
	[SerializeField] private float m_AttackTimer;
	[Range(0, 3)]
	[SerializeField] private float m_AttackRadius;
	
	[SerializeField]private Transform m_HandTransform;
	[SerializeField]private LayerMask m_LayerMask;

	private void Update()
	{
		MeleeAttack();
	}

	private void MeleeAttack()
	{
		m_AttackTimer -= Time.deltaTime;

		Collider2D collider = Physics2D.OverlapCircle(m_HandTransform.position, m_AttackRadius, m_LayerMask);

		if (Input.GetMouseButtonDown(0))
		{
			EventManager.Instance.MeleeAnimTrigger?.Invoke();

			if (collider == null)
			{
				return;
			}

			collider.gameObject.GetComponent<Enemy>().TakeDamage(m_Damage);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(m_HandTransform.position, m_AttackRadius);
	}
}
