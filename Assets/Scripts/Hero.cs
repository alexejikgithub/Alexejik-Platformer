using Platformer.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
	public class Hero : MonoBehaviour
	{

		[SerializeField] private float _speed;
		[SerializeField] private float _speedSprintMultiplier;
		[SerializeField] private float _jumpSpeed;
		[SerializeField] private float _wallJumpSpeed;
		[SerializeField] private float _damageJumpSpeed;
		[SerializeField] private float _fallingSpeedLimit;


		[SerializeField] private LayerMask _interactionLayer;
		[SerializeField] private CoinCounter _coinCunter;


		[SerializeField] private LayerCheck _groundCheck;
		[SerializeField] private LayerCheck _wallCheck;
		[SerializeField] private float _interactRadius;

		[SerializeField] private SpawnComponent _footStepParticles;
		[SerializeField] private SpawnComponent _jumpParticles;
		[SerializeField] private SpawnComponent _landingParticles;
		[SerializeField] private SpawnComponent _slideParticles;

		[SerializeField] private ParticleSystem _hitParticles;

		private Collider2D[] _interactResult = new Collider2D[1];
		private Rigidbody2D _rigidbody;
		private Vector2 _direction;
		private Animator _animator;
		private float _gravity; // controlls the speed of falling depending on the condition

		private bool _isGrounded;
		private bool _isTouchingWall;
		private bool _isSlidingOffTheWall;
		private bool _allowSecondJump;
		private bool _isJumping;
		private bool _isSprinting;
		private bool _isMakingWalljump;



		private static readonly int IsRunning = Animator.StringToHash("isRunning");
		private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
		private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
		private static readonly int Hit = Animator.StringToHash("hitTrigger");
		private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
		private static readonly int IsSliding = Animator.StringToHash("isSliding");

		private void Start()
		{
			_gravity = _rigidbody.gravityScale;
		}
		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();
			_isMakingWalljump = false;

		}

		public void SetDirection(Vector2 direction)
		{
			_direction = direction;
		}

		private void Update()
		{
			
			_isGrounded = IsGrounded();
			_isTouchingWall = IsTouchingWall(); // check if hero is touching the wall
			_isSlidingOffTheWall = _isTouchingWall && !_isGrounded; // if hero is falling down next to the wall, he will "stick" to it and will not be able to move away from it 

		}

		private void FixedUpdate()
		{
			SetGravity();
			float runningSpeed = _isSprinting ? (_speed * _speedSprintMultiplier) : _speed; //  sets the speed (sprint or normal)


			float xVelocity = 0;
			if (!_isSlidingOffTheWall) // hero cannot move if he is sliding down the wall and cannot change direction
			{
				xVelocity = _direction.x * runningSpeed;
				UpdateSpriteDirection();
			}

			if(_isSlidingOffTheWall) //Check ifthe player wants to unhook of the wall when sliding
			{
				StartCoroutine(UnhookOfTheWall());
			}

			var yVelocity = CalculateYVelocity();



			if (!_isMakingWalljump) // Movement via velocity is turned off when jumping off the wall
			{
				_rigidbody.velocity = new Vector2(xVelocity, yVelocity);
			}



			Debug.Log(_isSlidingOffTheWall);

			_animator.SetBool(IsRunning, _direction.x != 0);
			_animator.SetBool(IsSprinting, _isSprinting);
			_animator.SetBool(IsSliding, _isSlidingOffTheWall);
			_animator.SetBool(IsGroundedKey, _isGrounded);
			_animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);






		}
		private float CalculateYVelocity()
		{
			float yVelocity = _rigidbody.velocity.y;

			var isJumpPressing = _direction.y > 0;



			if (_isGrounded)
			{
				_allowSecondJump = true;
				_isJumping = false;
			}

			if (isJumpPressing)

				if (isJumpPressing)
				{
					_isJumping = true;
					yVelocity = CalculateJumpVelocity(yVelocity);


				}
				else if (_rigidbody.velocity.y > 0 && _isJumping)
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


		private void SetGravity() // sets gravity. 2 states 1- hero is sliding down the wall, 2- normal
		{

			if (_isSlidingOffTheWall && _rigidbody.velocity.y < 0)
			{
				_rigidbody.gravityScale = _gravity / 4f;
			}
			else
			{
				_rigidbody.gravityScale = _gravity;
			}




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

		private bool IsTouchingWall()
		{
			return _wallCheck.IsTouchingLayer;
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

		public void SetIsSprinting(bool state)
		{
			_isSprinting = state;

		}

		public void TakeDamage()
		{
			_isJumping = false;
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

		public void SpawnlandingDust()
		{
			Debug.Log(_rigidbody.velocity.y);

			if (_rigidbody.velocity.y < -_fallingSpeedLimit)
			{
				_landingParticles.Spawn();
			}

		}
		public void SpawnSlideDust()
		{
			_slideParticles.Spawn();
		}

		private IEnumerator JumpOffTheWall(float vectorX) // this method is used to make a jump if hero is touching the wall . called from HeroInputReader
		{
			if (_isSlidingOffTheWall) // Jumping off the wall only if _isTouchingWall && !_isGrounded
			{

				_isMakingWalljump = true;
				_rigidbody.velocity = new Vector2(0, 0);

				_rigidbody.AddForce(new Vector2(-vectorX, 2) * _wallJumpSpeed, ForceMode2D.Impulse);
				transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				yield return new WaitForSeconds(0.5f);
				_isMakingWalljump = false;
				yield return null;
			}


		}

		public void DoJumpOffTheWall() //public use of JumpOffTheWall
		{
			StartCoroutine(JumpOffTheWall(transform.localScale.x));
		}

		private IEnumerator UnhookOfTheWall() //this methods allows to unhook of the wall if you hold direction key for a while
		{

			if(_direction.x==-transform.localScale.x)
			{
				yield return new WaitForSeconds(0.15f);
			}
			if(_direction.x == -transform.localScale.x)
			{

				_wallCheck.GetComponent<Collider2D>().enabled = false;
				yield return new WaitForSeconds(0.1f);
				_wallCheck.GetComponent<Collider2D>().enabled = true;

			}

		}
	}
}
