using Platformer.Components.ColliderBased;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.Creatures.Mobs.Patrolling
{
	public class PlatformPatrol : Patrol
	{
		private Creature _creature;

		[SerializeField] private ChooseDirection _chooseDirection;

		private float _startDirection => _chooseDirection == ChooseDirection.Right ? 1f : -1f;
		private float _direction;
		[SerializeField] private LayerCheck _platformCheck;

		private void Start()
		{
			_direction = _startDirection;
		}

		private enum ChooseDirection
		{
			Right,
			Left
		}



		private void Awake()
		{
			_creature = GetComponent<Creature>();
		}

		public override IEnumerator DoPatrol()
		{
			while (enabled)
			{
				if (!_platformCheck.IsTouchingLayer)
				{
					
					_direction *= -1f;
					
				}
				var direction = new Vector2(_direction, 0);

				_creature.SetDirection(direction.normalized);
				yield return null;
			}
		}



	}


}