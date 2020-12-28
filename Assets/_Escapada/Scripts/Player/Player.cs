using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[Header("Health")]
	[Range(1, 3)]
	[SerializeField] private int m_MaxHealth;
	[SerializeField] private int m_Health;

	[Header("UI")]
	[SerializeField] private Slider m_HealthBar;

	[Header("UI")]
	[SerializeField] private ParticleSystem m_HurtParticle;

	public static bool IsAttacking;
	public static bool IsLookingLeft;

	private void Awake()
	{
		Subscribe();
		Init();
	}

	private void Init()
	{
		m_Health = m_MaxHealth;
		m_HealthBar.maxValue = m_MaxHealth;
		m_HealthBar.value = m_MaxHealth;
	}

	private void OnApplicationQuit()
	{
		Unsubscribe();
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.HurtPlayer += Hurt;
		EventManager.Instance.HealPlayer += Heal;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.HurtPlayer -= Hurt;
		EventManager.Instance.HealPlayer -= Heal;
	}

	#endregion

	private void Heal(int healingHealth, Action targetFunc)
	{
		if (CheckIsHealthFull())
		{
			return;
		}

		m_Health += healingHealth;

		m_HealthBar.value = m_Health;

		targetFunc();
	}

	private void Hurt(int takenDamage)
	{
		PlayParticle();

		EventManager.Instance.HurtAnimTrigger?.Invoke();

		m_Health -= takenDamage;

		m_HealthBar.value = m_Health;

		if (m_Health <= 0)
		{
			KillPlayer();
		}
	}

	private bool CheckIsHealthFull()
	{
		return m_Health == m_MaxHealth;
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;

		//if (layer == Layers.FOOD && !CheckIsHealthFull())
		//{
		//	RegenerateHealth();
		//}
	}

	private void KillPlayer()
	{
		EventManager.Instance.DeathAnimTrigger?.Invoke(true);
	}
}
