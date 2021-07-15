using Platformer.Components.ColliderBased;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Creatures.Mobs.Patrolling
{
	public class PlatformPatrol : Patrol
	{
		

		[SerializeField] private ChooseDirection _chooseDirection;

		private float _startDirection => _chooseDirection == ChooseDirection.Right ? 1f : -1f;
		private float _direction;
		[SerializeField] private LineCheck _platformCheck;
		[SerializeField] private LineCheck _obstacleCheck;

		[SerializeField] private OnChangeDirection _OnchangeDirection;

		private void Start()
		{
			_direction = _startDirection;
		}

		private enum ChooseDirection
		{
			Right,
			Left
		}




		public override IEnumerator DoPatrol()
		{
			while (enabled)
			{
				if (_platformCheck.IsTouchingLayer&&!_obstacleCheck.IsTouchingLayer)
				{

					_OnchangeDirection?.Invoke(new Vector2(_direction, 0));

				}
				else
				{
					_direction = -_direction;
					_OnchangeDirection?.Invoke(new Vector2(_direction, 0));
				}
				

				
				yield return null;
			}
		}

		[Serializable]
		public class OnChangeDirection:UnityEvent<Vector2>
		{

		}

	}


}