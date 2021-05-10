using Platformer.Components.ColliderBased;
using Platformer.Components.GoBased;
using Platformer.Creatures.Mobs.Patrolling;
using System.Collections;
using UnityEngine;

namespace Platformer.Creatures.Mobs
{
	public class BaseMobAI : MonoBehaviour
	{

		[SerializeField] protected ColliderCheck _vision;
		[SerializeField] protected ColliderCheck _canAttack;

		[SerializeField] private float _alarmDelay = 0.5f;
		[SerializeField] protected float _attackCooldown = 1f;
		[SerializeField] protected float _missHeroCooldown = 1f;

		protected IEnumerator _current;
		protected GameObject _target;

		protected static readonly int IsDeadkKey = Animator.StringToHash("is-dead");

		protected SpawnListComponent _particles;


		protected Animator _animator;
		protected bool _isDead;
		protected Patrol _patrol;

		protected Creature _creature;

		protected virtual void Awake()
		{
			_particles = GetComponent<SpawnListComponent>();
			_animator = GetComponent<Animator>();
			_patrol = GetComponent<Patrol>();
			_creature = GetComponent<Creature>();
		}

		protected void Start()
		{
			StartState(_patrol.DoPatrol());
		}


		protected void StartState(IEnumerator corutine)
		{
			_creature.SetDirection(Vector2.zero);
			if (_current != null)
			{
				StopCoroutine(_current);
			}
			_current = corutine;
			StartCoroutine(corutine);
		}

		public void OnHeroInVision(GameObject go)
		{
			if (_isDead) return;
			_target = go;

			StartState(AgroToHero());
		}
		protected IEnumerator AgroToHero()
		{
			LookAtHero();
			_particles?.Spawn("Exclamation");
			yield return new WaitForSeconds(_alarmDelay);
			StartState(GoToHero());
		}
		protected void LookAtHero()
		{
			var direction = GetDirectionToTarget();
			_creature.UpdateSpriteDirection(direction);
		}
		protected virtual IEnumerator GoToHero()
		{
			yield return null;
		}
		protected IEnumerator Attack()
		{
			while (_canAttack.IsTouchingLayer)
			{
				_creature.Attack();
				yield return new WaitForSeconds(_attackCooldown);
			}
			StartState(GoToHero());
		}

		protected virtual Vector2 GetDirectionToTarget()
		{
			var direction = _target.transform.position - transform.position;
			direction.y = 0;
			return direction.normalized;
		}
		protected void SetDirectionToTarget()
		{
			var direction = GetDirectionToTarget();
			_creature.SetDirection(direction);
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

		protected virtual void ChangeCapsuleColliderOnDeath()
		{

		}

	}
}