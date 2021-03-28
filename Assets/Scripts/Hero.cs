using Platformer.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
	public class Hero : MonoBehaviour
	{

		[SerializeField] private float _speed;
		[SerializeField] private float _jumpSpeed;
		[SerializeField] private float _damageJumpSpeed;


		[SerializeField] private LayerMask _interactionLayer;
		[SerializeField] private CoinCounter _coinCunter;


		[SerializeField] private LayerCheck _groundCheck;
		[SerializeField] private float _interactRadius;

		[SerializeField] private SpawnComponent _footStepParticles;
		[SerializeField] private SpawnComponent _jumpParticles;

		[SerializeField] private ParticleSystem _hitParticles;

		private Collider2D[] _interactResult = new Collider2D[1];
		private Rigidbody2D _rigidbody;
		private Vector2 _direction;
		private Animator _animator;

		private bool _isGrounded;
		private bool _allowSecondJump;
		


		private static readonly int IsRunning = Animator.StringToHash("isRunning");
		private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
		private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
		private static readonly int Hit = Animator.StringToHash("hitTrigger");
		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();
			

		}

		public void SetDirection(Vector2 direction)
		{
			_direction = direction;
		}

		private void Update()
		{
			_isGrounded = IsGrounded();

		}

		private void FixedUpdate()
		{
			var xVelocity = _direction.x * _speed;
			var yVelocity = CalculateYVelocity();

			_rigidbody.velocity = new Vector2(xVelocity, yVelocity);




			_animator.SetBool(IsRunning, _direction.x != 0);
			_animator.SetBool(IsGroundedKey, _isGrounded);
			_animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);

			UpdateSpriteDirection();


		}
		private float CalculateYVelocity()
		{
			float yVelocity = _rigidbody.velocity.y;

			var isJumpPressing = _direction.y > 0;

			if (_isGrounded)
			{
				_allowSecondJump = true;
				
			}

			if (isJumpPressing)
			{
				
				yVelocity = CalculateJumpVelocity(yVelocity);


			}
			else if (_rigidbody.velocity.y > 0)
			{
				yVelocity *= 0.5f;

			}

			return yVelocity;

		}
		private float CalculateJumpVelocity(float yVelocity)
		{
			bool isFalling = _rigidbody.velocity.y <= 0.001f;

			if (!isFalling) return yVelocity;

			if (_isGrounded)
			{
				SpawnJumpDust();
				yVelocity = _jumpSpeed;

			}
			else if (_allowSecondJump)
			{
				SpawnJumpDust();
				yVelocity = _jumpSpeed;
				_allowSecondJump = false;
			}
			return yVelocity;
		}




		private void UpdateSpriteDirection()
		{
			if (_direction.x > 0)
			{
				transform.localScale = Vector3.one;


			}
			else if (_direction.x < 0)
			{
				transform.localScale = new Vector3(-1, 1, 1);


			}

		}

		private bool IsGrounded()
		{
			return _groundCheck.IsTouchingLayer;

		}

		private void OnDrawGizmos() //Checking ray for jumping
		{
			Gizmos.color = IsGrounded() ? Color.green : Color.red;
			Gizmos.DrawSphere(transform.position, 0.3f);
		}


		public void SaySomehting()
		{
			Debug.Log("Somehing");
		}

		public void TakeDamage()
		{
			_animator.SetTrigger(Hit);
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed);

			if (_coinCunter.GiveCoinAmount() > 0)
			{
				SpawnCoins();
			}


		}
		public void SpawnCoins()
		{
			var numCoinsToDispose = Mathf.Min(_coinCunter.GiveCoinAmount(), 5);
			_coinCunter.RemoveCoinsFromCounter(numCoinsToDispose);
			var burst = _hitParticles.emission.GetBurst(0);
			burst.count = numCoinsToDispose;

			_hitParticles.emission.SetBurst(0, burst);

			_hitParticles.gameObject.SetActive(true);
			_hitParticles.Play();
		}

		public void Interact()
		{
			var size = Physics2D.OverlapCircleNonAlloc(
				transform.position,
				_interactRadius,
				_interactResult,
				_interactionLayer);

			for (int i = 0; i < size; i++)
			{
				var interactable = _interactResult[i].GetComponent<InteractableComponent>();
				if (interactable != null)
				{
					interactable.Intract();
				}
			}

		}
		public void SpawnFootDust()
		{
			_footStepParticles.Spawn();
		}

		public void SpawnJumpDust()
		{
			
			
				_jumpParticles.Spawn();

			


		}



	}

}
