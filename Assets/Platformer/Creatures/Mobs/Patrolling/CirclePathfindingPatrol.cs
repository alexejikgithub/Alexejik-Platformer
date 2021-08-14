using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Creatures.Mobs.Patrolling
{
	public class CirclePathfindingPatrol : Patrol
	{
		[SerializeField] private AIDestinationSetter _destinationSetter;
		[SerializeField] private float _circleRadius;
		[SerializeField] private int _numberOfPoints;
		[SerializeField] private float _treshold = 1f;
		[SerializeField] private GameObject _patrolTarget;

		private int _destinationPointIndex;

		private Vector2[] _patrolPoints;

		public Vector2[] PatrolPoints => _patrolPoints;

		private void Awake()
		{
			CalculatePositions();
		}

		// Sets an array of Vector2s for _patrolPoints.
		private void CalculatePositions()
		{
			var step = 2 * Mathf.PI / _numberOfPoints;
			
			Vector2 centerPosition = transform.position;
			_patrolPoints = new Vector2[_numberOfPoints];
			for (var i = 0; i < _numberOfPoints; i++)
			{
				var angle = step * i;
				var pos = new Vector2(
					Mathf.Cos(angle) * _circleRadius,
					Mathf.Sin(angle) * _circleRadius
					);
				_patrolPoints[i] = centerPosition + pos;
			}
		}

#if UNITY_EDITOR
		private void OnValidate()
		{

			CalculatePositions();
		}

		// Draws the patrol Area.
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, _circleRadius);
			Gizmos.color = Color.red;
			foreach (var point in _patrolPoints)
			{
				Gizmos.DrawLine(point, new Vector2(point.x + 0.5f, point.y + 0.5f));

			}

		}
#endif
		[ContextMenu("Patrol")]
		public void StartPartol()
		{
			StartCoroutine(DoPatrol());
		}
		public override IEnumerator DoPatrol()
		{
			while (enabled)
			{
				if (IsOnPoint())
				{
					_destinationPointIndex = UnityEngine.Random.Range(0, _numberOfPoints-1);
				}
				
				_patrolTarget.transform.position = _patrolPoints[_destinationPointIndex];
				_destinationSetter.target = _patrolTarget.transform;
				yield return null;
			}
		}

		// Checks if patrol point is reached.
		private bool IsOnPoint()
		{
			
			return (_patrolPoints[_destinationPointIndex] - new Vector2(_destinationSetter.transform.position.x, _destinationSetter.transform.position.y)).magnitude < _treshold;
		}


	}
}
