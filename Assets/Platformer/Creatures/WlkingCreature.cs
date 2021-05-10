using Platformer.Components;
using Platformer.Components.Audio;
using Platformer.Components.ColliderBased;
using Platformer.Components.GoBased;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures

{

	public class WlkingCreature : Creature
	{
		[Header("Params")]
		[SerializeField] protected float JumpSpeed;
		[SerializeField] private float _damageVelocity;


		[Header("Checkers")]
		[SerializeField] protected LayerMask GroundLayer;
		[SerializeField] private ColliderCheck _groundCheck;



		protected bool IsGrounded;
		private bool _isJumping;


		private static readonly int IsRunning = Animator.StringToHash("isRunning");
		private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
		private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");



		protected override void Awake()
		{
			base.Awake();
		}



		protected virtual void Update()
		{
			IsGrounded = _groundCheck.IsTouchingLayer;
		}



		protected override void FixedUpdate()
		{

			base.FixedUpdate();
			Animator.SetBool(IsRunning, Direction.x != 0);
			Animator.SetBool(IsGroundedKey, IsGrounded);
			Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
		}
		protected override float CalculateYVelocity()
		{
			float yVelocity = Rigidbody.velocity.y;

			var isJumpPressing = Direction.y > 0;

			if (IsGrounded)
			{

				_isJumping = false;
			}

			if (isJumpPressing)
			{
				_isJumping = true;

				bool isFalling = Rigidbody.velocity.y <= 0.001f;
				yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
			}
			else if (Rigidbody.velocity.y > 0 && _isJumping)
			{
				yVelocity *= 0.5f;
			}
			return yVelocity;
		}
		protected virtual float CalculateJumpVelocity(float yVelocity)
		{

			if (IsGrounded)
			{
				DoJumpVfx();
				yVelocity = JumpSpeed;
			}
			return yVelocity;
		}

		protected void DoJumpVfx()
		{
			Particles.Spawn("Jump");
			Sounds.Play("Jump");
		}

		public override void TakeDamage()
		{
			_isJumping = false;
			base.TakeDamage();
			Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
		}



	}
}