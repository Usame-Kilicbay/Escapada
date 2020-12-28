using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeapon : Projectile
{
	[Space(10)]
	[Range(0f,100f)]
	[SerializeField] private float m_RollSpeed;

	protected override void Vanish()
	{
		Destroy(gameObject);
	}

	private void Update()
	{
		Roll();
	}

	private void Roll()
	{
		transform.Rotate(Vector3.forward * m_RollSpeed);
	}
}
