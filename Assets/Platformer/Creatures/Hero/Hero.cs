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

		//[SerializeField] private CoinCounter _coinCunter;

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

		private int SwordCount => _session.Data.Inventory.Count("Sword");
		private int CoinsCount => _session.Data.Inventory.Count("Coin");


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
			_session.Data.Inventory.OnChanged += OnInventoryChanged;
			_session.Data.Inventory.OnChanged += AnotherHandler;
		}
		void OnDestroy()
		{
			_session.Data.Inventory.OnChanged -= OnInventoryChanged;
			_session.Data.Inventory.OnChanged -= AnotherHandler;
		}
		private void AnotherHandler(string id, int value)
		{
			Debug.Log($"Inventory changed: {id}: {value}");

		}

		private void OnInventoryChanged(string id, int value)
		{
			Debug.Log(Animator);
			if (id == "Sword")
			{

				UpdateHeroWeapon();
			}
		}

		public void AddInInventory(string id, int value)
		{
			_session.Data.Inventory.Add(id, value);
		}

		internal IEnumerator ThrowBunch()
		{
			int swordCount = SwordCount > 3 ? 2 : SwordCount - 2;

			if (swordCount >= 0)
			{
				
				Throw();

				while (swordCount > 0)
				{
					_session.Data.Inventory.Remove("Sword", 1);
					yield return new WaitForSeconds(0.2f);
					swordCount--;
					OnDoThrow();

				}

			}
		}

		public void Throw()
		{
			if (_throwCooldown.IsReady && SwordCount > 1)
			{
				Animator.SetTrigger(ThrowKey);
				_throwCooldown.Reset();
				_session.Data.Inventory.Remove("Sword", 1);
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
			Debug.Log(CoinsCount);
			if (CoinsCount > 0)
			{
				SpawnCoins();
			}

		}
		public void SpawnCoins()
		{
			var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
			_session.Data.Inventory.Remove("Coins", numCoinsToDispose);

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


			if (SwordCount <= 0) return;
			base.Attack();


		}

		private void UpdateHeroWeapon()
		{

			Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _unarmed;

		}
		
	}

}
