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
using Platformer.Components.GoBased;
using Assets.Platformer.Model.Data;
using Platformer.UI.GameMenu;
using UnityEngine.InputSystem;
using Platformer.Model.Definitions;
using Platformer.UI.MainMenu;

namespace Platformer.Creatures.Hero

{
	public class Hero : Creature, ICanAddInInventory
	{


		// [SerializeField] private float _speedSprintMultiplier;


		[SerializeField] private float _fallingSpeedLimit;
		[SerializeField] private ColliderCheck _wallCheck;



		[SerializeField] private CheckCircleOverlap _interactionCheck;

		//[SerializeField] private CoinCounter _coinCunter;

		[SerializeField] private Cooldown _throwCooldown;
		[SerializeField] private AnimatorController _armed;
		[SerializeField] private AnimatorController _unarmed;

		[Space]
		[Header("Super throw")]
		[SerializeField] private Cooldown _SuperThrowCooldown;
		[SerializeField] private int _superThrowParticles;
		[SerializeField] private float _superThrowDelay;
		[SerializeField] private SpawnComponent _throwSpawner;

		[Space]

		[SerializeField] private ProbabilityDropComponent _hitDrop;




		private static readonly int ThrowKey = Animator.StringToHash("throw");



		private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");



		private bool _allowSecondJump;
		private bool _isOnWall;
		private bool _superThrow;
		private bool _isSpeedUp;

		//private bool _isSprinting;


		private GameMenuWindow _gameMenu;

		private GameSession _session;
		private float _defautGravityScale;


		private const string SwordId = "Sword";
		private int SwordCount => _session.Data.Inventory.Count(SwordId);
		private int CoinsCount => _session.Data.Inventory.Count("Coin");
		private int HealPotionCount => _session.Data.Inventory.Count("HealPotion");
		private int SpeedPotionCount => _session.Data.Inventory.Count("SpeedPotion");

		private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

		private bool CanThrow
		{
			get
			{
				if (SelectedItemId == SwordId)
					return SwordCount > 1;


				var def = DefsFacade.I.Items.Get(SelectedItemId);

				return def.HasTag(ItemTag.Throwable);

			}
		}

		protected override void Awake()
		{
			base.Awake();
			_defautGravityScale = Rigidbody.gravityScale;
		}



		private void Start()
		{
			_session = FindObjectOfType<GameSession>();

			var health = GetComponent<HealthComponent>();
			health.SetHealth(_session.Data.Hp.Value);
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

			if (id == SwordId)
			{

				UpdateHeroWeapon();
			}
		}

		public void AddInInventory(string id, int value)
		{
			_session.Data.Inventory.Add(id, value);
		}
		public void StartThrowing()
		{

			_SuperThrowCooldown.Reset();
		}
		public void PerformThrowing()
		{
			if (!_throwCooldown.IsReady || !CanThrow) return;
			if (_SuperThrowCooldown.IsReady) _superThrow = true;

			Animator.SetTrigger(ThrowKey);
			_throwCooldown.Reset();
		}



		public void OnDoThrow()
		{
			if (_superThrow)
			{
				var throwableCount = _session.Data.Inventory.Count(SelectedItemId);
				var possibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;
				var numThrows = Mathf.Min(_superThrowParticles, possibleCount);
				StartCoroutine(DoSuperThrow(numThrows));

			}
			else
			{
				ThrowAndRemoveFromInventory();
			}
			_superThrow = false;


		}

		private IEnumerator DoSuperThrow(int numThrows)
		{
			for (int i = 0; i < numThrows; i++)
			{
				ThrowAndRemoveFromInventory();
				yield return new WaitForSeconds(_superThrowDelay);
			}
		}

		private void ThrowAndRemoveFromInventory()
		{
			Sounds.Play("Range");



			var throwableId = _session.QuickInventory.SelectedItem.Id;

			var throwableDef = DefsFacade.I.ThrowableItems.Get(throwableId);
			_throwSpawner.SetPrefab(throwableDef.Projectile);
			_throwSpawner.Spawn();
			_session.Data.Inventory.Remove(throwableId, 1);

		}

		public void OnHealthChanged(int currentHealth)
		{
			_session.Data.Hp.Value = currentHealth;
			Debug.Log(currentHealth);
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
			if (_isOnWall) //   !isJumpPressing && убрал
			{
				return 0f;
			}



			return base.CalculateYVelocity();

		}
		protected override float CalculateJumpVelocity(float yVelocity)
		{
			if (!IsGrounded && _allowSecondJump && !_isOnWall)
			{
				DoJumpVfx();
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

		public void DrinkPotion()
		{
			var def = DefsFacade.I.Items.Get(SelectedItemId);
			var defId = def.Id;

			Debug.Log(defId);
			if (defId == "HealPotion" && HealPotionCount > 0)
			{
				var health = GetComponent<HealthComponent>();
				health.ApplyHealing(5);
				_session.Data.Inventory.Remove("HealPotion", 1);
			}
			if (defId == "SpeedPotion" && SpeedPotionCount > 0 && !_isSpeedUp)
			{

				StartCoroutine(SpeedUp());



			}
		}

		private IEnumerator SpeedUp()
		{
			_isSpeedUp = true;
			_speedMultiplier *= 2;
			_session.Data.Inventory.Remove("SpeedPotion", 1);
			yield return new WaitForSeconds(5);
			_speedMultiplier = 1;

			_isSpeedUp = false;
		}

		public void SpawnCoins()
		{
			var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
			_session.Data.Inventory.Remove("Coin", numCoinsToDispose);

			_hitDrop.SetCount(numCoinsToDispose);
			_hitDrop.CalculateDrop();
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

		public void OpenGameMenu()
		{

			if (_gameMenu == null)
			{
				var window = Resources.Load<GameObject>("UI/GameMenuWindow");
				var canvas = FindObjectOfType<MainCanvas>();
				_gameMenu = Instantiate(window, canvas.transform).GetComponent<GameMenuWindow>();

			}
			else
			{
				_gameMenu.OnContinue();

			}

		}
		internal void NextItem()
		{
			_session.QuickInventory.SetNextItem();
		}

	}

}
