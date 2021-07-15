using Platformer.Creatures.Mobs.Patrolling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Creatures.Mobs.Boss.Slime
{
	public class SlimeAI : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Vector2 _direction;
		[SerializeField] private Rigidbody2D _ridgitbody;
		[SerializeField] private Patrol _patrol;

		private void Start()
		{
			StartCoroutine(_patrol.DoPatrol());
		}
		private void FixedUpdate()
		{
			_ridgitbody.velocity = _direction * _speed;
		}

		public void SetDirection(Vector2 direction)
		{
			_direction = direction;
		}

		private void Update()
		{
			if (_direction.x > 0)
			{
				transform.localScale = new Vector3(-1, 1, 1);
			}
				
			else if (_direction.x < 0)
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
		}

	}


}
