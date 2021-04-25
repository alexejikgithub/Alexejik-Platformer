using Platformer.Components;
using Platformer.Components.ColliderBased;
using Platformer.Components.GoBased;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures

{

	public class Creature : MonoBehaviour
	{
		[Header("Params")]
		[SerializeField] private bool _invertScale;
		[SerializeField] protected float Speed;
		[SerializeField] protected float JumpSpeed;
		[SerializeField] private float _damageJumpSpeed;
		[SerializeField] private float _damageVelocity;


		[Header("Checkers")]
		[SerializeField] protected LayerMask GroundLayer;
		[SerializeField] private ColliderCheck _groundCheck;
		[SerializeField] private CheckCircleOverlap _attackRange;
		[SerializeField] protected SpawnListComponent Particles;

		protected Rigidbody2D Rigidbody;
		protected Vector2 Direction;
		protected Animator Animator;
		protected bool IsGrounded;
		private bool _isJumping;


		private static readonly int IsRunning = Animator.StringToHash("isRunning");
		private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
		private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
		private static readonly int Hit = Animator.StringToHash("hitTrigger");
		private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
		private static readonly int AttackKey = Animator.StringToHash("attack");


		protected virtual void Awake()
		{
			Rigidbody = GetComponent<Rigidbody2D>();
			Animator = GetComponent<Animator>();
		}

		public void SetDirection(Vector2 direction)
		{
			Direction = direction;
		}

		protected virtual void Update()
		{
			IsGrounded = _groundCheck.IsTouchingLayer;
		}


		protected virtual void FixedUpdate()
		{
			//float runningSpeed = _isSprinting ? (_speed * _speedSprintMultiplier) : _speed;
			var xVelocity = Direction.x * Speed;


			var yVelocity = CalculateYVelocity();

			Rigidbody.velocity = new Vector2(xVelocity, yVelocity);




			Animator.SetBool(IsRunning, Direction.x != 0);
			//_animator.SetFloat(IsSprinting, _isSprinting ? 1f : 0f);
			Animator.SetBool(IsGroundedKey, IsGrounded);
			Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);

			UpdateSpriteDirection(Direction);
		}
		protected virtual float CalculateYVelocity()
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
				Particles.Spawn("Jump");
				yVelocity = JumpSpeed;
			}
			return yVelocity;
		}

		public void UpdateSpriteDirection(Vector2 direction)
		{
			var multiplier = _invertScale ? -1 : 1;
			if (direction.x > 0)
			{
				transform.localScale = new Vector3(multiplier, 1, 1);
			}
			else if (direction.x < 0)
			{
				transform.localScale = new Vector3(-1 * multiplier, 1, 1);
			}

		}
		public virtual void TakeDamage()
		{
			_isJumping = false;
			Animator.SetTrigger(Hit);
			Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
		}
		public virtual void Attack()
		{
			Animator.SetTrigger(AttackKey);
		}

		public void OnAttacking()
		{
			_attackRange.Check();
		}


	}
}