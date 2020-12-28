using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrower : MonoBehaviour
{
	//[SerializeField] private WeaponData m_CurrentWeapon;

	[Range(1, 5)]
	[SerializeField] private int m_ThrowingWeaponAmount;
	[Range(0, 1)]
	[SerializeField] private float m_ThrowingTimerLimit;
	[SerializeField] private float m_ThrowingTimer;

	[SerializeField] private Transform m_ThrowerTransform;
	[SerializeField] private GameObject m_ThrowingWeaponPrefab;

	private void Awake()
	{
		Subscribe();
		Init();
	}

	private void OnApplicationQuit()
	{
		Unsubscribe();
	}

	private void Init()
	{

	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{

	}

	private void Unsubscribe()
	{

	}

	#endregion

	private void Update()
	{
		Throw();
	}

	private void Throw()
	{
		m_ThrowingTimer -= Time.deltaTime;

		if (Input.GetMouseButtonDown(1) && m_ThrowingTimer <= 0)
		{
			EventManager.Instance.ThrowAnimTrigger?.Invoke();

			Instantiate(m_ThrowingWeaponPrefab, m_ThrowerTransform.position, Quaternion.identity);
			m_ThrowingTimer = m_ThrowingTimerLimit;
		}
	}

	private void GetThrownWeapon()
	{

	}
}
