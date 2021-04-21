using Platformer.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Utils;
using UnityEditor;
using UnityEditor.Animations;
using Platformer.Model;
using Platformer.Components.ColliderBased;
using Platformer.Components.Health;

namespace Platformer.Creatures.Hero

{
	public class Hero : Creature
	{


		// [SerializeField] private float _speedSprintMultiplier;


		[SerializeField] private float _fallingSpeedLimit;
		[SerializeField] private LayerCheck _wallCheck;



		[SerializeField] private CheckCircleOverlap _interactionCheck;

		[SerializeField] private CoinCounter _coinCunter;

		[SerializeField] private Cooldown _throwCooldown;
		[SerializeField] private AnimatorController _armed;
		[SerializeField] private AnimatorController _unarmed;


		[Space]
		[Header("Particles")]
		[SerializeField] private ParticleSystem _hitParticles;

		private static readonly int ThrowKey = Animator.StringToHash("throw");
		private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");



		private bool _allowSecondJump;
		private bool _isOnWall;
		//private bool _isSprinting;



		private GameSession _session;
		private float _defautGravityScale;


		protected override void Awake()
		{
			base.Awake();
			_defautGravityScale = Rigidbody.gravityScale;
		}



		private void Start()
		{
			_session = FindObjectOfType<GameSession>();

			var health = GetComponent<HealthComponent>();
			health.SetHealth(_session.Data.Hp);
			UpdateHeroWeapon();
		}

		internal IEnumerator ThrowBunch()
		{
			int swordCount = _session.Data.SwordsCount > 3 ? 2 : _session.Data.SwordsCount - 2;

			if (swordCount >= 0)
			{
				_session.Data.SwordsCount -= swordCount;
				Throw();

				while (swordCount > 0)
				{
					yield return new WaitForSeconds(0.2f);
					swordCount--;
					OnDoThrow();

				}

			}
		}

		public void Throw()
		{
			if (_throwCooldown.IsReady && _session.Data.SwordsCount > 1)
			{
				Animator.SetTrigger(ThrowKey);
				_throwCooldown.Reset();
				_session.Data.SwordsCount -= 1;
			}

		}

		public void OnDoThrow()
		{
			Particles.Spawn("Throw");
		}

		public void OnHealthChanged(int currentHealth)
		{
			_session.Data.Hp = currentHealth;
		}



		protected override void Update()
		{


			base.Update();

			var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;

			if (_wallCheck.IsTouchingLayer && moveToSameDirection)
			{
				_isOnWall = true;
				Rigidbody.gravityScale = 0;
			}
			else
			{
				_isOnWall = false;
				Rigidbody.gravityScale = _defautGravityScale;
			}
			Animator.SetBool(IsOnWall, _isOnWall);
		}

		protected override void FixedUpdate()
		{

			base.FixedUpdate();
		}


		protected override float CalculateYVelocity()
		{
			var isJumpPressing = Direction.y > 0;

			if (IsGrounded || _isOnWall)
			{
				_allowSecondJump = true;
			}
			if (!isJumpPressing && _isOnWall)
			{
				return 0f;
			}



			return base.CalculateYVelocity();

		}
		protected override float CalculateJumpVelocity(float yVelocity)
		{
			if (!IsGrounded && _allowSecondJump && !_isOnWall)
			{
				Particles.Spawn("Jump");
				_allowSecondJump = false;
				return JumpSpeed;
			}
			return base.CalculateJumpVelocity(yVelocity);
		}


		//public void SetIsSprinting(bool state)
		//{
		//	_isSprinting = state;

		//}

		public override void TakeDamage()
		{
			base.TakeDamage();
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
			_interactionCheck.Check();

		}

		private void OnCollisionEnter2D(Collision2D collision)
		{


			if (collision.gameObject.IsInLayer(GroundLayer))
			{

				var contact = collision.contacts[0];
				if (contact.relativeVelocity.y >= _fallingSpeedLimit)
				{

					Particles.Spawn("SlamDown");
				}

			}
		}

		public override void Attack()
		{
			if (!_session.Data.IsArmed) return;
			base.Attack();

		}


		public void ArmHero()
		{

			_session.Data.SwordsCount += 1;
			UpdateHeroWeapon();

		}

		private void UpdateHeroWeapon()
		{
			Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _unarmed;

		}


	}

}
