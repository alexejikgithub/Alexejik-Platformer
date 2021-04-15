using Platformer.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Utils;
using UnityEditor;
using UnityEditor.Animations;
using Platformer.Model;

namespace Platformer.Creatures

{
	public class Hero : Creature
	{


		// [SerializeField] private float _speedSprintMultiplier;


		[SerializeField] private float _fallingSpeedLimit;



		[SerializeField] private LayerMask _interactionLayer;
		[SerializeField] private float _interactRadius;
		[SerializeField] private CheckCircleOverlap _interactionCheck;

		[SerializeField] private CoinCounter _coinCunter;


		[SerializeField] private AnimatorController _armed;
		[SerializeField] private AnimatorController _unarmed;


		[Space]
		[Header("Particles")]
		[SerializeField] private ParticleSystem _hitParticles;



		private bool _allowSecondJump;
		//private bool _isSprinting;



		private GameSession _session;


		protected override void Awake()
		{
			base.Awake();
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



		protected override void Update()
		{


			base.Update();
		}

		protected override void FixedUpdate()
		{

			base.FixedUpdate();
		}


		protected override float CalculateYVelocity()
		{
			var isJumpPressing = Direction.y > 0;

			if (IsGrounded)
			{
				_allowSecondJump = true;
			}



			return base.CalculateYVelocity();

		}
		protected override float CalculateJumpVelocity(float yVelocity)
		{
			if (!IsGrounded && _allowSecondJump)
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

			_session.Data.IsArmed = true;
			UpdateHeroWeapon();

		}

		private void UpdateHeroWeapon()
		{
			Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _unarmed;

		}


	}

}
