using Pathfinding;
using Platformer.Animations;
using Platformer.Components.ColliderBased;
using Platformer.Components.GoBased;
using Platformer.Creatures.Mobs.Patrolling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Creatures.Mobs
{
	public class SpiderAI : MonoBehaviour
	{
		[SerializeField] private AIDestinationSetter _destinationSetter;
		[SerializeField] private SpriteAnimation _animator;
		[SerializeField] protected CheckCircleOverlap _attackRange;
		[SerializeField] private CirclePathfindingPatrol _patrol;

		[SerializeField] private ColliderCheck _vision;
		[SerializeField] private ColliderCheck _canAttack;
		[SerializeField] private Rigidbody2D _rb;

		[SerializeField] private float _alarmDelay = 0.1f;
		[SerializeField] private float _attackCooldown = 1f;
		[SerializeField] private float _missHeroCooldown = 1f;
		private IEnumerator _current;
		private GameObject _target;

		

		private SpawnListComponent _particles;
		private bool _isDead;

		public bool IsDead => _isDead;



		private void Awake()
		{
			_particles = GetComponent<SpawnListComponent>();
			
		}

		private void Start()
		{
			StartState(_patrol.DoPatrol());
		}

		public void OnHeroInVision(GameObject go)
		{
			if (_isDead) return;
			_target = go;

			StartState(AgroToHero());
		}

		private IEnumerator AgroToHero()
		{
			
			_particles.Spawn("Exclamation");
			yield return new WaitForSeconds(_alarmDelay);
			StartState(GoToHero());
		}

		private IEnumerator GoToHero()
		{
			if (!_isDead)
			{


				while (_vision.IsTouchingLayer)
				{
					if (_canAttack.IsTouchingLayer)
					{

						StartState(Attack());
					}
					else
					{
						_destinationSetter.target = _target.transform;
					}

					yield return null;
				}
				_particles.Spawn("MissHero");
				
				yield return new WaitForSeconds(_missHeroCooldown);
				StartState(_patrol.DoPatrol());

			}
		}
		private IEnumerator Attack()
		{
			while (_canAttack.IsTouchingLayer)
			{
				_attackRange.Check();
				_animator.SetClip("attack");
				yield return new WaitForSeconds(_attackCooldown);
			}
			StartState(GoToHero());
		}

		private void StartState(IEnumerator corutine)
		{
			
			if (_current != null)
			{
				StopCoroutine(_current);
			}
			_current = corutine;
			StartCoroutine(corutine);
		}

		public void OnDie()
		{
			
			// Creature stops after death.
			_isDead = true;
			_destinationSetter.target = null;
			_rb.bodyType = RigidbodyType2D.Dynamic;
			_rb.constraints = RigidbodyConstraints2D.FreezePositionX;

			if (_current != null)
			{
				StopCoroutine(_current);
			}

		}

	}
}
