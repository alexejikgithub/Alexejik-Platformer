using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Movement
{
	public class VerticalLevitatilonComponent : MonoBehaviour
	{
		[SerializeField] private float _frequency = 1f;
		[SerializeField] private float _amplitude = 1f;

		private float _originalY;
		private Rigidbody2D _rigidbody;
		private float _seed;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_originalY = _rigidbody.transform.position.y;
			_seed = Random.value * Mathf.PI * 2;
		}

		private void Update()
		{
			var pos = _rigidbody.position;
			pos.y = _originalY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
			_rigidbody.MovePosition(pos);
		}
	}
}
