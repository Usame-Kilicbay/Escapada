using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public struct Datas
{
	public float Distance;
	public float Health;
	public float Speed;

	public Pos PlayerPosition;

	public Inventory Inventory;

	public void Save() 
	{
	
	}
}

[Serializable]
public struct Pos
{
	public float PosX;
	public float PosY;
}

[Serializable]
public struct Inventory
{
	public int Capacity;
	public float Food;
	public float Drink;
	public float Torch;
}

public class SaveLoadManager : MonoBehaviour
{
	private void SaveDatas(int progress)
	{

	}

	private void SaveInventory() 
	{
		Datas datas = Load();
	}

	private void SavePosition(Vector2 playerPos)
	{
		Datas datas = Load();

		datas.PlayerPosition.PosX = playerPos.x;
		datas.PlayerPosition.PosY = playerPos.y;

		string json = JsonUtility.ToJson(datas);

		File.WriteAllText(Paths.DATA, json);
	}

	private Datas Load()
	{
		Datas datas = new Datas();

		if (File.Exists(Paths.DATA))
		{
			string json = File.ReadAllText(Paths.DATA);

			datas = JsonUtility.FromJson<Datas>(json);
		}

		return datas;
	}
}
