using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Create New Weapon Data")]
public class WeaponData : ScriptableObject
{
	public Texture2D WeaponArt;

	public string WeaponName;

	public float FireRate;

	public int Damage;
}
