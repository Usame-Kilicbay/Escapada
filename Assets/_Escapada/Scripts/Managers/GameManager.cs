using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
	Prepare,
	GameStarted,
	DeathTimer,
	LevelCompleted,
	LevelFailed,
}

public class GameManager : MonoBehaviour
{
	private static GameStates _gameState;
	public static GameStates GameState
	{
		get => _gameState;

		set
		{
			_gameState = value;

			if (_gameState == GameStates.LevelCompleted)
			{
				LevelCompleted();
			}
			else if (_gameState == GameStates.LevelFailed)
			{
				LevelFailed();
			}
		}
	}

	private static bool isQuitting;

	private void OnEnable()
	{
		GameState = GameStates.GameStarted;
	}

	private static void LevelCompleted()
	{
		EventManager.Instance.SaveLevelID();
		EventManager.Instance.LevelCompleted();
	}
	private static void LevelFailed()
	{
		EventManager.Instance.LevelFailed();
	}

	public static void QuitControl(Action targetMethod)
	{
		if (isQuitting)
		{
			return;
		}

		targetMethod();
	}

	private void OnApplicationQuit()
	{
		isQuitting = true;
	}
}
