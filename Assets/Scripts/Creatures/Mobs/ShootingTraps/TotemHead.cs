using Platformer.Components.GoBased;
using Platformer.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs.ShootingTraps

{
	[System.Serializable]
	public class TotemHead : MonoBehaviour
	{
		[SerializeField] private SpawnComponent _rangeAttack;

		private static readonly int Range = Animator.StringToHash("range");

		private Animator _animator;

		private TotemScript _totemScript;

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
			var collider = gameObject.GetComponent<BoxCollider2D>();
			float offsetMultiplier = -(count - 1) / 2;
			collider.offset = new Vector2(collider.offset.x, collider.offset.y + (collider.size.y * offsetMultiplier));
			collider.size = new Vector2(collider.size.x, collider.size.y * count);

		}
		public void DoTurnOnTopCollider()
		{
			_totemScript.TurnOnTopCollider();
		}



	}
}