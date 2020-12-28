using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Walk/Run")]

	[Range(0f, 30f)]
	[SerializeField] private float m_WalkSpeed;
	[Range(0f, 10f)]
	[SerializeField] private float m_RunSpeed;

	[Header("Jump")]
	[Range(0f, 100f)]
	[SerializeField] private float m_JumpForce;
	[Range(0f, 100f)]
	[SerializeField] private float m_FallSpeed;
	[Range(0f, 1f)]
	[SerializeField] private float m_CheckRadius;
	[Range(0, 2)]
	[SerializeField] private int m_ExtraJump;
	[Space(5)]
	[SerializeField] private LayerMask m_LayerMask;
	[SerializeField] private Transform m_GroundCheckTransform;
	[SerializeField] private bool m_IsGrounded;


	[Header("Components")]
	[SerializeField] private SpriteRenderer m_SpriteRenderer;
	[SerializeField] private Rigidbody2D m_Rigidbody2D;
	[SerializeField] private BoxCollider2D m_BoxCollider2D;

	private void FixedUpdate()
	{
		Walk();
	}

	private void Update()
	{
		Jump();
	}

	private void Walk()
	{
		float moveInput = Input.GetAxis("Horizontal");

		m_Rigidbody2D.velocity = new Vector2(moveInput * m_WalkSpeed, m_Rigidbody2D.velocity.y);

		if (moveInput < 0)
		{
			Player.IsLookingLeft = true;

			transform.localScale = new Vector2
			{
				x = -1,
				y = transform.localScale.y
			};
		}
		else if(moveInput > 0)
		{
			Player.IsLookingLeft = false;

			transform.localScale = new Vector2
			{
				x = 1,
				y = transform.localScale.y
			};
		}
		else
		{
			transform.localScale = transform.localScale;
		}

		EventManager.Instance.SpeedAnimTrigger(Mathf.Abs(m_Rigidbody2D.velocity.x));
	}

	private void Run()
	{

	}

	private void Jump()
	{
		m_IsGrounded = Physics2D.OverlapCircle(m_GroundCheckTransform.position, m_CheckRadius, m_LayerMask);

		if (!m_IsGrounded && m_ExtraJump <= 0)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space) && m_ExtraJump > 0)
		{
			EventManager.Instance.JumpAnimTrigger?.Invoke();

			m_Rigidbody2D.velocity = Vector2.up * m_JumpForce;
			m_ExtraJump--;
		}
	}

	private void Fall()
	{
		m_Rigidbody2D.velocity -= Vector2.down * Time.deltaTime * m_FallSpeed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject collidedObject = collision.gameObject;

		if (collidedObject.layer == Layers.PLATFORM)
		{
			m_ExtraJump = 2;
			m_IsGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		GameObject collidedObject = collision.gameObject;

		//if (collidedObject.layer == 11)
		//{

		//}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(m_GroundCheckTransform.position, m_CheckRadius);
	}
}
