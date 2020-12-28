using System;
using System.Collections;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
	// Game
	public Action<GameObject> VanishBullet;

	#region Player

	public Action<int> HurtPlayer;
	public Action<int, Action> HealPlayer;
	public Action<int> UpdatePlayerHealthBar;
	public Action<string> ChangeAnimState;
	public Func<IEnumerator> ResetAnimState;

	public Action<float> SpeedAnimTrigger;
	public Action JumpAnimTrigger;
	public Action MeleeAnimTrigger;
	public Action ThrowAnimTrigger;
	public Action HurtAnimTrigger;
	public Action<bool> DeathAnimTrigger;

	#endregion

	// UI
	public Action UpdateLevelText;

	#region Level

	public Func<int> GetSavedLevelID;
	public Action SaveLevelID;
	public Action LoadLevel;

	#endregion

	// Result
	public Action LevelFailed;
	public Action LevelCompleted;
}
