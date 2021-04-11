using Platformer.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Utils;
using UnityEditor;
using UnityEditor.Animations;
using Platformer.Model;

namespace Platformer
{
	public class Hero : MonoBehaviour
	{

		[SerializeField] private float _speed;
		[SerializeField] private float _speedSprintMultiplier;
		[SerializeField] private float _jumpSpeed;
		[SerializeField] private float _damageJumpSpeed;
		[SerializeField] private float _fallingSpeedLimit;
		[SerializeField] private int _damage;

		[SerializeField] private LayerMask _groundLayer;
		[SerializeField] private LayerMask _interactionLayer;
		[SerializeField] private float _interactRadius;


		[SerializeField] private CoinCounter _coinCunter;


		[SerializeField] private LayerCheck _groundCheck;

		[SerializeField] private CheckCircleOverlap _attackRange;


		[SerializeField] private AnimatorController _armed;
		[SerializeField] private AnimatorController _unarmed;


		[Space]
		[Header("Particles")]
		[SerializeField] private SpawnComponent _footStepParticles;
		[SerializeField] private SpawnComponent _jumpParticles;
		[SerializeField] private SpawnComponent _landingParticles;
		[SerializeField] private SpawnComponent _swordParticles;

		[SerializeField] private ParticleSystem _hitParticles;

		private Collider2D[] _interactResult = new Collider2D[1];
		private Rigidbody2D _rigidbody;
		private Vector2 _direction;
		private Animator _animator;

		private bool _isGrounded;
		private bool _allowSecondJump;
		private bool _isJumping;
		private bool _isSprinting;





		private static readonly int IsRunning = Animator.StringToHash("isRunning");
		private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
		private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
		private static readonly int Hit = Animator.StringToHash("hitTrigger");
		private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
		private static readonly int AttackKey = Animator.StringToHash("attack");

		private GameSession _session;


		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();


		}

		
		private void Start()
		{
			_session = FindObjectOfType<GameSession>();

			var health = GetComponent<HealthComponent>();
			health.SetHealth(_session.Data.Hp);
			UpdateHeroWeapon();
		}
		public void OnHealthChanged(int currentHealth)
		{
			_session.Data.Hp = currentHealth;
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
			float runningSpeed = _isSprinting ? (_speed * _speedSprintMultiplier) : _speed;

			var xVelocity = _direction.x * runningSpeed;
			var yVelocity = CalculateYVelocity();

			_rigidbody.velocity = new Vector2(xVelocity, yVelocity);




			_animator.SetBool(IsRunning, _direction.x != 0);
			_animator.SetFloat(IsSprinting, _isSprinting ? 1f : 0f);
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
				_isJumping = false;
			}

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

#if UNITY_EDITOR

		private void OnDrawGizmos() //Checking ray for jumping
		{


			Handles.color = IsGrounded() ? HandlesUtils.TransparentGreen : HandlesUtils.TransparentRed;
			Handles.DrawSolidDisc(transform.position, Vector3.forward, 0.3f);
		}
#endif

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
		public void Attack()
		{
			if (!_session.Data.IsArmed) return;
			_animator.SetTrigger(AttackKey);

		}
		public void OnAttacking()
		{
			var gos = _attackRange.GetObjectInRange();
			foreach (var go in gos)
			{
				var hp = go.GetComponent<HealthComponent>();
				if (hp != null && go.CompareTag("Enemy"))
				{
					hp.ApplyDamage(_damage);
				}
			}
		}

		public void ArmHero()
		{

			_session.Data.IsArmed = true;
			UpdateHeroWeapon();

		}

		private void UpdateHeroWeapon()
		{
			_animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _unarmed;
			
		}

		public void SpawnFootDust()
		{
			_footStepParticles.Spawn();
		}

		public void SpawnJumpDust()
		{
			_jumpParticles.Spawn();
		}
		public void SpawnSwordDust()
		{
			_swordParticles.Spawn();
		}


		private void OnCollisionEnter2D(Collision2D collision)
		{


			if (collision.gameObject.IsInLayer(_groundLayer))
			{

				var contact = collision.contacts[0];
				if (contact.relativeVelocity.y >= _fallingSpeedLimit)
				{

					SpawnlandingDust();
				}

			}
		}
		public void SpawnlandingDust()
		{

			_landingParticles.Spawn();

		}



	}

}
