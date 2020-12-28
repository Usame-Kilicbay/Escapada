using Constants;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header("Values")]
	[Range(1, 5)]
	[SerializeField] private int m_Health;

	[Range(0f, 5f)]
	[SerializeField] private float m_MovementDuration;

	[Range(0f, 4f)]
	[SerializeField] private float m_PatrolDistance;

	[Range(0f, 10f)]
	[SerializeField] private float m_SightDistance;

	[Range(0f, 20f)]
	[SerializeField] private float m_ThrowSpeed;
	
	[Space(10)]
	[SerializeField] private float m_AttackCountdownLimit;
	[SerializeField] private float m_AttackCountdown;

	[Space(10)]
	[SerializeField] private LayerMask m_LayerMask;

	[Header("Components")]
	[SerializeField] private SpriteRenderer m_SpriteRenderer;
	[SerializeField] private Animator m_Animator;

	[Header("Childs")]
	[SerializeField] private Transform m_HandTransform;
	[SerializeField] private ParticleSystem m_HurtParticle;

	[Header("Prefabs")]
	[SerializeField] private GameObject m_PopCornPrefab;

	private float m_PatrolMaxLimit;
	private float m_PatrolMinLimit;

	private bool m_IsLookingLeft;
	private bool m_IsAttacking;

	private Sequence m_PatrolLeftSequence;
	private Sequence m_PatrolRightSequence;

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		m_PatrolMinLimit = transform.position.x - m_PatrolDistance;
		m_PatrolMaxLimit = transform.position.x + m_PatrolDistance;

		m_AttackCountdown = m_AttackCountdownLimit;

		if (UnityEngine.Random.value < 0.5)
		{
			StartCoroutine(PatrolLeft());
		}
		else
		{
			StartCoroutine(PatrolRight());
		}
	}

	#region Patrol

	private IEnumerator PatrolLeft()
	{
		yield return new WaitWhile(() => m_IsAttacking);

		transform.localScale = new Vector2(-1f, transform.localScale.y);
		m_IsLookingLeft = true;

		m_PatrolLeftSequence = DOTween.Sequence();

		m_PatrolLeftSequence.Append(transform.DOMoveX(m_PatrolMinLimit, m_MovementDuration));
		m_PatrolLeftSequence.OnComplete(() => StartCoroutine(PatrolRight()));
	}

	private IEnumerator PatrolRight()
	{
		yield return new WaitWhile(() => m_IsAttacking);

		transform.localScale = new Vector2(1f, transform.localScale.y);
		m_IsLookingLeft = false;

		m_PatrolRightSequence = DOTween.Sequence();

		m_PatrolRightSequence.Append(transform.DOMoveX(m_PatrolMaxLimit, m_MovementDuration));
		m_PatrolRightSequence.OnComplete(() => StartCoroutine(PatrolLeft()));
	}

	#endregion

	private void Update()
	{
		Attack();
	}

	private void Attack()
	{
		m_AttackCountdown -= Time.deltaTime;

		bool isEnemyInSight = Physics2D.Raycast(transform.position, GetDirection(), m_SightDistance, m_LayerMask).collider;

		if (m_AttackCountdown > 0 || !isEnemyInSight)
		{
			m_IsAttacking = false;

			return;
		}

		m_Animator.SetTrigger(CornAnimationParameters.THROW);

		GameObject newPopCorn = Instantiate(m_PopCornPrefab, m_HandTransform);
		//newPopCorn.transform.localPosition = Vector3.zero;

		Debug.Log("fırlattı");

		if (transform.localScale.x == -1)
		{
			newPopCorn.GetComponent<Rigidbody2D>().AddForce(Vector2.left * m_ThrowSpeed, ForceMode2D.Impulse);
		}
		else
		{
			newPopCorn.GetComponent<Rigidbody2D>().AddForce(Vector2.right * m_ThrowSpeed, ForceMode2D.Impulse);
		}

		m_AttackCountdown = m_AttackCountdownLimit;
	}

	private Vector2 GetDirection()
	{
		return m_IsLookingLeft ? Vector2.left : Vector2.right;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;

		if (layer == Layers.BULLET || layer == Layers.THROWING_WEAPON)
		{
			TakeDamage(1);
		}
	}

	public void TakeDamage(int takenDamage)
	{
		m_Health -= takenDamage;

		PlayParticle();

		if (m_Health <= 0)
		{
			StartCoroutine(SelfDestruct());
		}
	}

	private void PlayParticle()
	{
		m_HurtParticle.transform.localScale = transform.localScale;

		if (m_HurtParticle.isPlaying)
		{
			m_HurtParticle.Stop();
		}

		m_HurtParticle.Play();
	}

	private IEnumerator SelfDestruct()
	{
		m_PatrolRightSequence.Kill();
		m_PatrolLeftSequence.Kill();

		yield return new WaitWhile(()=> m_HurtParticle.isPlaying);

		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Vector3 m_PatrolMinLimit = new Vector3(transform.position.x - m_PatrolDistance, transform.position.y - 0.5f);
		Vector3 m_PatrolMaxLimit = new Vector3(transform.position.x + m_PatrolDistance, transform.position.y - 0.5f);

		Gizmos.color = Color.green;
		Gizmos.DrawLine(m_PatrolMinLimit, m_PatrolMaxLimit);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.left * m_SightDistance);
	}
}
