using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
	[SerializeField] private Transform m_PlayerTransform;
	[SerializeField] private float m_Distance;

	[SerializeField] private TextMeshProUGUI m_DistanceText;

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		m_Distance = 0f;
		UpdateDistanceText();
	}

	private void Update()
	{
		if (m_PlayerTransform.position.x > m_Distance)
		{
			m_Distance = m_PlayerTransform.position.x;

			UpdateDistanceText();
		}
	}

	private void UpdateDistanceText() 
	{
		m_DistanceText.SetText(m_Distance.ToString("##"));
	}
}
