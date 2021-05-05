using Platformer.Components.GoBased;
using Platformer.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs.ShootingTraps

{
	[RequireComponent(typeof(Animator))]
	public class TotemHead : MonoBehaviour
	{
		[SerializeField] private SpawnComponent _rangeAttack;

		private static readonly int Range = Animator.StringToHash("range");

		private Animator _animator;

		private TotemScript _totemScript;

		private BoxCollider2D _collider;

		public BoxCollider2D Collider
		{
			get
			{
				if (!_collider)
				{
					_collider = GetComponent<BoxCollider2D>();
				}
				return _collider;
			}
		}

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_totemScript = GetComponentInParent<TotemScript>();

		}

		[ContextMenu("Attack")]
		public void RangeAttack()
		{
			_animator.SetTrigger(Range);
		}


		public void OnRangeAttack()
		{
			_rangeAttack.Spawn();
		}
		public void StreachAndCenterCollider(int count)
		{

			float offsetMultiplier = -(count - 1) / 2f;

			Collider.offset = new Vector2(Collider.offset.x, Collider.offset.y + (Collider.size.y * offsetMultiplier));
			Collider.size = new Vector2(Collider.size.x, Collider.size.y * count);

		}
		public void DoTurnOnTopCollider()
		{
			_totemScript.TurnOnTopCollider();
		}

		public void PlayHitAnimation()
		{
			_animator.SetTrigger("hit");
		}


	}
}