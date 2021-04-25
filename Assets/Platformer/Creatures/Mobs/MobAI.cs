using Platformer.Components;
using Platformer.Components.ColliderBased;
using Platformer.Components.GoBased;
using Platformer.Creatures.Mobs.Patrolling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs
{

	public class MobAI : MonoBehaviour
	{
		[SerializeField] private ColliderCheck _vision;
		[SerializeField] private ColliderCheck _canAttack;

		[SerializeField] private float _alarmDelay = 0.5f;
		[SerializeField] private float _attackCooldown = 1f;
		[SerializeField] private float _missHeroCooldown = 1f;
		private IEnumerator _current;
		private GameObject _target;

		private static readonly int IsDeadkKey = Animator.StringToHash("is-dead");

		private SpawnListComponent _particles;
		private Creature _creature;
		private Animator _animator;
		private bool _isDead;
		private Patrol _patrol;

		private void Awake()
		{
			_particles = GetComponent<SpawnListComponent>();
			_creature = GetComponent<Creature>();
			_animator = GetComponent<Animator>();
			_patrol = GetComponent<Patrol>();
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
			LookAtHero();
			_particles.Spawn("Exclamation");
			yield return new WaitForSeconds(_alarmDelay);
			StartState(GoToHero());
		}

		private void LookAtHero()
		{
			var direction = GetDirectionToTarget();
			_creature.UpdateSpriteDirection(direction);
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
						SetDirectionToTarget();
					}

					yield return null;
				}
				_particles.Spawn("MissHero");
				_creature.SetDirection(Vector2.zero);
				yield return new WaitForSeconds(_missHeroCooldown);
				StartState(_patrol.DoPatrol());

			}
		}

		private IEnumerator Attack()
		{
			while (_canAttack.IsTouchingLayer)
			{
				_creature.Attack();
				yield return new WaitForSeconds(_attackCooldown);
			}
			StartState(GoToHero());
		}

		private void SetDirectionToTarget()
		{
			var direction = GetDirectionToTarget();
			_creature.SetDirection(direction);
		}

		private Vector2 GetDirectionToTarget()
		{
			var direction = _target.transform.position - transform.position;
			direction.y = 0;
			return direction.normalized;
		}



		private void StartState(IEnumerator corutine)
		{
			_creature.SetDirection(Vector2.zero);
			if (_current != null)
			{
				StopCoroutine(_current);
			}
			_current = corutine;
			StartCoroutine(corutine);
		}

		public void OnDie()
		{
			_creature.SetDirection(new Vector2(0, 0));
			// Creature stops after death.
			_isDead = true;
			_animator.SetBool(IsDeadkKey, true);
			if (_current != null)
			{
				StopCoroutine(_current);
			}

		}
		public void ChangeCapsuleColliderOnDeath()
		{
			var collider = GetComponent<CapsuleCollider2D>();
			collider.direction = CapsuleDirection2D.Horizontal;
			collider.offset = Vector2.zero;
			collider.size = new Vector2(0.78f, 0.47f);
		}
	}
}