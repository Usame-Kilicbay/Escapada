using Constants;
using System;
using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	[SerializeField] private Animator m_Animator;

	[SerializeField] private string m_CurrentState;

	private void Awake()
	{
		Subscribe();
		Init();
	}

	private void Init()
	{
		m_CurrentState = PlayerAnimationStates.IDLE;
	}

	private void OnApplicationQuit()
	{
		Unsubscribe();
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.ChangeAnimState += ChangeAnimState;
		EventManager.Instance.ResetAnimState += ResetAnimState;

		EventManager.Instance.SpeedAnimTrigger += SpeedAnimTrigger;
		EventManager.Instance.JumpAnimTrigger += JumpAnimTrigger;
		EventManager.Instance.MeleeAnimTrigger += MeleeAnimTrigger;
		EventManager.Instance.ThrowAnimTrigger += ThrowAnimTrigger;
		EventManager.Instance.HurtAnimTrigger += HurtAnimTrigger;
		EventManager.Instance.DeathAnimTrigger += DeathAnimTrigger;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.ChangeAnimState -= ChangeAnimState;
		EventManager.Instance.ResetAnimState -= ResetAnimState;

		EventManager.Instance.SpeedAnimTrigger -= SpeedAnimTrigger;
		EventManager.Instance.JumpAnimTrigger -= JumpAnimTrigger;
		EventManager.Instance.MeleeAnimTrigger -= MeleeAnimTrigger;
		EventManager.Instance.ThrowAnimTrigger -= ThrowAnimTrigger;
		EventManager.Instance.HurtAnimTrigger -= HurtAnimTrigger;
		EventManager.Instance.DeathAnimTrigger -= DeathAnimTrigger;
	}

	#endregion

	#region Triggers

	private void SpeedAnimTrigger(float speed)
	{
		m_Animator.SetFloat(PlayerAnimationParameters.SPEED, speed);
	}

	private void JumpAnimTrigger()
	{
		m_Animator.SetTrigger(PlayerAnimationParameters.JUMP);
	}

	private void MeleeAnimTrigger()
	{
		m_Animator.SetTrigger(PlayerAnimationParameters.MELEE);
	}

	private void ThrowAnimTrigger()
	{
		m_Animator.SetTrigger(PlayerAnimationParameters.THROW);
	}

	private void HurtAnimTrigger()
	{
		m_Animator.SetTrigger(PlayerAnimationParameters.HURT);
	}

	private void DeathAnimTrigger(bool isDead)
	{
		m_Animator.SetBool(PlayerAnimationParameters.DEATH, isDead);
	}

	#endregion

	private float GetAnimLenght()
	{
		return m_Animator.GetCurrentAnimatorStateInfo(0).length;
	}

	private IEnumerator ResetAnimState(/*bool isLooping*/)
	{
		yield return new WaitForSeconds(GetAnimLenght());

		//if (isLooping)
		//{
		//	yield return new WaitUntil(m_Animator.);
		//}

		//ChangeAnimState(PlayerAnimationStates.IDLE);
	}

	private void ChangeAnimState(string state)
	{
		//if (m_CurrentState.Equals(state))
		//{
		//	return;
		//}

		//m_Animator.Play(state);

		//m_CurrentState = state;
	}
}
