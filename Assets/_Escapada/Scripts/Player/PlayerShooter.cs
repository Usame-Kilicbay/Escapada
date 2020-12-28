using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
	[SerializeField] private WeaponData m_CurrentWeapon;

	[SerializeField] private Transform m_WeaponTransform;

	[Range(0, 100)]
	[SerializeField] private int m_BulletAmount;
	[SerializeField] private List<GameObject> m_Bullets;

	[SerializeField] private GameObject m_BulletPrefab;

	[SerializeField] private float m_ShootingTimer;

	private void Awake()
	{
		Subscribe();
		Init();
	}

	private void OnApplicationQuit()
	{
		Unsubscribe();
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.VanishBullet += VanishBullet;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.VanishBullet -= VanishBullet;
	}

	#endregion

	private void Init()
	{
		for (int i = 0; i < m_BulletAmount; i++)
		{
			GameObject newBullet = Instantiate(m_BulletPrefab, m_WeaponTransform.position, Quaternion.identity);
			newBullet.transform.SetParent(transform);
			newBullet.name += i;
			m_Bullets.Add(newBullet);
			newBullet.SetActive(false);
		}
	}

	private void Update()
	{
		Shoot();
	}

	private void Shoot()
	{
		m_ShootingTimer -= Time.deltaTime;

		if (Input.GetMouseButton(1) && m_ShootingTimer <= 0)
		{
			GameObject tempBullet = m_Bullets[0];
			tempBullet.SetActive(true);
			m_Bullets.Remove(tempBullet);
			m_ShootingTimer = m_CurrentWeapon.FireRate;
		}
	}

	private void VanishBullet(GameObject bullet)
	{
		bullet.SetActive(false);
		m_Bullets.Add(bullet);
		bullet.transform.localPosition = m_WeaponTransform.position;
	}
}
