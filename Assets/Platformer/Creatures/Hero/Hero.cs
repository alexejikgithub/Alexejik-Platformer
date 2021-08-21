using Platformer.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Utils;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
#endif

using Platformer.Model;
using Platformer.Components.ColliderBased;
using Platformer.Components.Health;
using Platformer.Components.GoBased;
using Assets.Platformer.Model.Data;
using Platformer.UI.GameMenu;
using UnityEngine.InputSystem;
using Platformer.Model.Definitions;
using Platformer.UI.MainMenu;
using Platformer.Model.Definitions.Repositories.Items;
using Platformer.Model.Definitions.Repositories;
using Platformer.UI.Windows.Perks;
using Platformer.Model.Definitions.Player;
using Platformer.Model.Data.Properties;
using Platformer.Effects.CameraRelated;
using UnityEngine.Analytics;


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
		[SerializeField] private RuntimeAnimatorController _armed;
		[SerializeField] private RuntimeAnimatorController _unarmed;

		[Space]
		[Header("Super throw")]
		[SerializeField] private Cooldown _SuperThrowCooldown;
		[SerializeField] private int _superThrowParticles;
		[SerializeField] private float _superThrowDelay;
		[SerializeField] private SpawnComponent _throwSpawner;

		[Space]

		[SerializeField] private ProbabilityDropComponent _hitDrop;
		// [SerializeField] private GameObject _shield;
		[SerializeField] private ShieldComponent _shield;
		[SerializeField] private HeroCandleController _candleController;



		private static readonly int ThrowKey = Animator.StringToHash("throw");



		private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");

		

		private bool _allowSecondJump;
		private bool _isOnWall;
		private bool _superThrow;

		private bool DoubleJumpPerkCanBePerformed => _session.PerksModel.IsDoubleJumpSupported; //&& _session.PerksModel.PerkIsReady;
		private bool SuperThrowpPerkCanBePerformed => _session.PerksModel.IsSuperThrowSupported; //&& _session.PerksModel.PerkIsReady;
		private bool ShieldPerkCanBePerformed => _session.PerksModel.IsShieldSupported;// && _session.PerksModel.PerkIsReady;
		//private bool _isSpeedUp;

		//private bool _isSprinting;


		private GameMenuWindow _gameMenu;

		private GameSession _session;
		private float _defautGravityScale;

		private Cooldown _speedUpCooldown = new Cooldown();

		private CameraShakeEffect _cameraShake;
		private float _additionalSpeed;

		private const string SwordId = "Sword";
		private int SwordCount => _session.Data.Inventory.Count(SwordId);
		private int CoinsCount => _session.Data.Inventory.Count("Coin");
		//private int HealPotionCount => _session.Data.Inventory.Count("HealPotion");
		//private int SpeedPotionCount => _session.Data.Inventory.Count("SpeedPotion");

		PerksDisplayWidget _perksDisplay;

		HealthComponent _health;

		private string SelectedItemId => _session.QuickInventory.SelectedItem != null ? _session.QuickInventory.SelectedItem.Id : "null";

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
			_session = GameSession.Instance;
			_perksDisplay = FindObjectOfType<PerksDisplayWidget>();
			_cameraShake = FindObjectOfType<CameraShakeEffect>();

			_health = GetComponent<HealthComponent>();
			_health.SetHealth(_session.Data.Hp.Value);
			UpdateHeroWeapon();
			_session.Data.Inventory.OnChanged += OnInventoryChanged;
			_session.Data.Inventory.OnChanged += AnotherHandler;
			_session.StatsModel.OnUpgraded += OnHeroUpgraded;
		}

		private void OnHeroUpgraded(StatId stateId)
		{
			switch (stateId)
			{
				case StatId.Hp:
					var health = (int) _session.StatsModel.GetValue(stateId);
					_session.Data.Hp.Value = health;
					_health.SetHealth(health);
					break;
			}
		}



		void OnDestroy()
		{
			_session.Data.Inventory.OnChanged -= OnInventoryChanged;
			_session.Data.Inventory.OnChanged -= AnotherHandler;
			_session.StatsModel.OnUpgraded -= OnHeroUpgraded;
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
		public void UseInventory()
		{

			if (IsSelectedItem(ItemTag.Throwable))
			{
				PerformThrowing();
			}
			else if (IsSelectedItem(ItemTag.Potion))
			{
				UsePotion();
			}
		}

		private void UsePotion()
		{

			var potion = DefsFacade.I.Potions.Get(SelectedItemId);
			switch (potion.Effect)
			{

				case Effect.AddHp:
					Debug.Log("heal");
					int maxHealth = (int)_session.StatsModel.GetValue(StatId.Hp);
					int healAmount = System.Math.Min((int)potion.Value, maxHealth - _health.Health);
					_health.ApplyHealing(healAmount); // переделал, чтобы работало

					break;

				case Effect.SpeedUp:
					_speedUpCooldown.Value = _speedUpCooldown.RemainingTime + potion.Time;
					_additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
					_speedUpCooldown.Reset();
					break;

			}

			_session.Data.Inventory.Remove(potion.Id, 1);
		}



		protected override float CalculateSpeed()
		{

			if (_speedUpCooldown.IsReady)
			{
				_additionalSpeed = 0f;
			}

			var defaultSpeed = _session.StatsModel.GetValue(StatId.Speed);
			return defaultSpeed + _additionalSpeed;
		}

		private bool IsSelectedItem(ItemTag tag)
		{
			return _session.QuickInventory.SelectedDef.HasTag(tag);
		}

		private void PerformThrowing()
		{
			if (!_throwCooldown.IsReady || !CanThrow) return;
			if (_SuperThrowCooldown.IsReady) _superThrow = true;

			Animator.SetTrigger(ThrowKey);
			_throwCooldown.Reset();
		}

		private int _superThrowCount;
		public void OnDoThrow()
		{
			if (_superThrow && SuperThrowpPerkCanBePerformed)
			{
				///*_perksDisplay.PerkReload()*/;
				var throwableCount = _session.Data.Inventory.Count(SelectedItemId);
				var possibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;
				var numThrows = Mathf.Min(_superThrowParticles, possibleCount);
				_session.PerksModel.Cooldown.Reset();
				StartCoroutine(DoSuperThrow(numThrows));
				_superThrowCount++;
				AnalyticsEvent.Custom("use-super-throw", new Dictionary<string, object>
				{
					{ "count",_superThrowCount },
					
				}
				);

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
			var instance = _throwSpawner.SpawnInstance();
			AplyRangeDamageStat(instance);
			_session.Data.Inventory.Remove(throwableId, 1);

		}

		private void AplyRangeDamageStat(GameObject projectile)
		{
			var hpModify = projectile.GetComponent<ChangeHealthComponent>();
			var value = (int) _session.StatsModel.GetValue(StatId.RangeDamage);
			value = ModifyDamageByCrit(value);
			hpModify.SetAmount(value); 
		}

		private int ModifyDamageByCrit(int damage)
		{
			var critChanse = _session.StatsModel.GetValue(StatId.CritDamage);
			if(Random.value*100<= critChanse)
			{
				return damage * 2;
			}
			return damage;
		}

		public void OnHealthChanged(int currentHealth)
		{
			_session.Data.Hp.Value = currentHealth;

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
			if (!IsGrounded && _allowSecondJump && DoubleJumpPerkCanBePerformed && !_isOnWall)
			{
				///*_perksDisplay.PerkReload()*/;
				DoJumpVfx();
				_allowSecondJump = false;
				_session.PerksModel.Cooldown.Reset();
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
			_cameraShake.Shake();
			if (CoinsCount > 0)
			{
				SpawnCoins();
			}

		}

		//public void DrinkPotion()
		//{
		//	var def = DefsFacade.I.Items.Get(SelectedItemId);
		//	var defId = def.Id;


		//	if (defId == "HealPotion" && HealPotionCount > 0)
		//	{
		//		var health = GetComponent<HealthComponent>();
		//		health.ApplyHealing(5);
		//		_session.Data.Inventory.Remove("HealPotion", 1);
		//	}
		//	if (defId == "SpeedPotion" && SpeedPotionCount > 0 && !_isSpeedUp)
		//	{
		//		StartCoroutine(SpeedUp());



		//	}
		//}

		//private IEnumerator SpeedUp()
		//{
		//	_isSpeedUp = true;
		//	_speedMultiplier *= 2;
		//	_session.Data.Inventory.Remove("SpeedPotion", 1);
		//	yield return new WaitForSeconds(5);
		//	_speedMultiplier = 1;

		//	_isSpeedUp = false;
		//}

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
		protected override void OnAttacking()
		{
			var hpModify = _attackRange.GetComponent<ChangeHealthComponent>();
			var defaultValue = hpModify.HealthPointsToChange;
			var value = ModifyDamageByCrit(defaultValue);
			hpModify.SetAmount(value);

			_attackRange.Check();
			hpModify.SetAmount(defaultValue);

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

		public void UseShield()
		{
			if (ShieldPerkCanBePerformed)
			{
				_shield.Use();
				_session.PerksModel.Cooldown.Reset();
			}
		}
		//public IEnumerator UseShield()
		//{
		//	if (ShieldPerkCanBePerformed)
		//	{
		//		//_perksDisplay.PerkReload();
		//		_session.PerksModel.Cooldown.Reset();
		//		_shield.SetActive(true);
		//		_health.IsInvinsible = true;
		//		yield return new WaitForSeconds(2);
		//		for (int i = 0; i < 5; i++)
		//		{

		//			_shield.SetActive(false);
		//			yield return new WaitForSeconds(0.1f);
		//			_shield.SetActive(true);
		//			yield return new WaitForSeconds(0.1f);
		//		}
		//		_shield.SetActive(false);
		//		_health.IsInvinsible = false;

		//	}
		//}

		internal void DropDown()
		{
			var endPosition = transform.position + new Vector3(0, -1);
			var hit = Physics2D.Linecast(transform.position, endPosition, GroundLayer);
			if (hit.collider == null) return;
			var component = hit.collider.GetComponent<TmpDisableComponent>();
			if (component == null) return;
			component.DisableCollider();

		}

		public void SwichLight()
		{
			_candleController.SwichLight();
		}
	}

}
