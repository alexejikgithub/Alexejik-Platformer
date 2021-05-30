using System.Collections;
using UnityEngine;

namespace Platformer.Effects
{
	public class ParalaxEffect : MonoBehaviour
	{
		[SerializeField] private float _effectValue;
		[SerializeField] private Transform _followTarget;
		private float _startX;
		private void Start()
		{
			_startX = transform.position.x;
		}

		private void LateUpdate()
		{
			var currentPosition = transform.position;
			var deltaX = _followTarget.position.x * _effectValue;
			transform.position = new Vector2(_startX + deltaX, currentPosition.y);
		}

	}
}