using Constants;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	private void OnEnable()
	{
		Subscribe();
	}

	private void OnDisable()
	{
		GameManager.QuitControl(Unsubscribe);
	}

	#region Event Subscribe

	private void Subscribe()
	{
		EventManager.Instance.GetSavedLevelID += GetSavedLevelID;
		EventManager.Instance.SaveLevelID += SaveLevelID;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.GetSavedLevelID -= GetSavedLevelID;
		EventManager.Instance.SaveLevelID -= SaveLevelID;
	}

	#endregion

	private int GetSavedLevelID()
	{
		if (UnityEngine.PlayerPrefs.HasKey(Constants.PlayerPrefs.LEVEL))
		{
			return UnityEngine.PlayerPrefs.GetInt(Constants.PlayerPrefs.LEVEL);
		}

		return 1;
	}

	private void SaveLevelID()
	{
		UnityEngine.PlayerPrefs.SetInt(Constants.PlayerPrefs.LEVEL, GetSavedLevelID() + 1);
	}
}
