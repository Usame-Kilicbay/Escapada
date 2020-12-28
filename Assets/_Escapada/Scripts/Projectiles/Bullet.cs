using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
	protected override void Vanish()
	{
		EventManager.Instance.VanishBullet(gameObject);
	}
}
