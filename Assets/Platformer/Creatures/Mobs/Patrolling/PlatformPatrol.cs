using Platformer.Components.ColliderBased;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.Creatures.Mobs.Patrolling
{
	public class PlatformPatrol : Patrol
	{
		private WlkingCreature _creature;

		[SerializeField] private ChooseDirection _chooseDirection;

		private float _startDirection => _chooseDirection == ChooseDirection.Right ? 1f : -1f;
		private float _direction;
		[SerializeField] private LineCheck _platformCheck;
		[SerializeField] private LineCheck _obstacleCheck;

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
			_creature = GetComponent<WlkingCreature>();
		}

		public override IEnumerator DoPatrol()
		{
			while (enabled)
			{
				if (_platformCheck.IsTouchingLayer&&!_obstacleCheck.IsTouchingLayer)
				{

					_creature.SetDirection(new Vector2(_direction, 0));

				}
				else
				{
					_direction = -_direction;
					_creature.SetDirection(new Vector2(_direction, 0));
				}
				

				
				yield return null;
			}
		}



	}


}