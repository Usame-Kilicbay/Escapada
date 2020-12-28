using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private SpriteRenderer m_SpriteRenderer;

	[Header("Parent Transforms")]
	[SerializeField] private Transform m_EnemyParentTransform;
	[SerializeField] private Transform m_TrapParentTransform;
	[SerializeField] private Transform m_FoodParentTransform;

	[Space(10)]
	[SerializeField] private Transform m_PlayerTransform;

	[Header("Prefabs")]
	[SerializeField] private List<GameObject> m_TrapPrefabs;

	[Space(10)]
	[SerializeField] private List<GameObject> m_FoodPrefabs;

	[Space(10)]
	[SerializeField] private List<GameObject> m_EnemyPrefabs;

	private float m_PlatformMinPosX;
	private float m_PlatformMaxPosX;
	private float m_PlatformMaxPosY;
	private Bounds m_Bounds;

	private bool m_IsTeleported;

	private void Awake()
	{
		Init();
	}

	private void OnEnable()
	{
		SpawnEnemies();
		SpawnTraps();
		SpawnFoods();
	}

	private void Init()
	{
		m_Bounds = m_SpriteRenderer.bounds;
		m_PlatformMinPosX = m_Bounds.min.x;
		m_PlatformMaxPosX = m_Bounds.max.x;
		m_PlatformMaxPosY = m_Bounds.max.y;
	}

	private void Update()
	{
		if (transform.position.x + m_Bounds.size.x / 2 <= m_PlayerTransform.position.x && !m_IsTeleported)
		{
			Debug.Log(m_PlatformMaxPosX);
			StartCoroutine(SelfTeleport());
		}
	}

	#region Spawn

	private void SpawnEnemies()
	{
		int randomIndex = Random.Range(0, m_EnemyPrefabs.Count);
		GameObject newEnemy = m_EnemyPrefabs[randomIndex];

		Bounds bounds = newEnemy.GetComponent<SpriteRenderer>().bounds;

		Vector2 spawnPos = new Vector2
		{
			x = GetRandomPos(bounds.size.x),
			y = m_PlatformMaxPosY + bounds.min.y
		};

		newEnemy = Instantiate(newEnemy, spawnPos, Quaternion.identity);
		newEnemy.transform.SetParent(m_EnemyParentTransform);
	}

	private void SpawnTraps()
	{
		int randomIndex = Random.Range(0, m_TrapPrefabs.Count);
		GameObject newTrap = m_TrapPrefabs[randomIndex];

		Bounds bounds = newTrap.GetComponent<SpriteRenderer>().bounds;

		Vector2 spawnPos = new Vector2
		{
			x = GetRandomPos(bounds.size.x),
			y = m_PlatformMaxPosY + Random.Range(0f, 3f)
		};

		newTrap = Instantiate(newTrap, spawnPos, Quaternion.identity);
		newTrap.transform.SetParent(m_TrapParentTransform);
	}

	private void SpawnFoods()
	{
		int randomIndex = Random.Range(0, m_FoodPrefabs.Count);
		GameObject newFood = m_FoodPrefabs[randomIndex];

		Bounds bounds = newFood.GetComponent<SpriteRenderer>().bounds;

		Vector2 spawnPos = new Vector2
		{
			x = GetRandomPos(bounds.size.x),
			y = Random.Range(1f, 3f)
		};

		newFood = Instantiate(newFood, spawnPos, Quaternion.identity);
		newFood.transform.SetParent(m_FoodParentTransform);
	}

	#endregion

	private float GetRandomPos(float size)
	{
		return Random.Range(m_PlatformMinPosX + size / 2f, m_PlatformMaxPosX - size / 2f);
	}

	private IEnumerator SelfTeleport()
	{
		m_IsTeleported = true;

		yield return new WaitForSeconds(3);

		transform.position += Vector3.right * transform.position.x * 3f;

		m_IsTeleported = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		int layer = collision.gameObject.layer;

		//if (layer == Layers.PLAYER)
		//{
		//	StartCoroutine(SelfTeleport());
		//}
	}
}
