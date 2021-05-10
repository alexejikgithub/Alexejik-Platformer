using Platformer.Components.Audio;
using Platformer.Components.ColliderBased;
using Platformer.Components.GoBased;
using System.Collections;
using UnityEngine;


namespace Platformer.Creatures

{
	public class Creature : MonoBehaviour
	{

		[Header("Params")]
		[SerializeField] private bool _invertScale;
		[SerializeField] protected float Speed;

		[Header("Checkers")]
		[SerializeField] private CheckCircleOverlap _attackRange;
		[SerializeField] protected SpawnListComponent Particles;


		protected Rigidbody2D Rigidbody;
		protected Vector2 Direction;
		protected Animator Animator;
		protected PlaySoundsComponent Sounds;


		private static readonly int Hit = Animator.StringToHash("hitTrigger");
		private static readonly int AttackKey = Animator.StringToHash("attack");




		protected virtual void Awake()
		{
			Rigidbody = GetComponent<Rigidbody2D>();
			Animator = GetComponent<Animator>();
			Sounds = GetComponent<PlaySoundsComponent>();
		}

		protected virtual void FixedUpdate()
		{

			var xVelocity = CalculateXVelocity();


			var yVelocity = CalculateYVelocity();

			Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

			UpdateSpriteDirection(Direction);
		}
		public void UpdateSpriteDirection(Vector2 direction)
		{
			var multiplier = _invertScale ? -1 : 1;
			if (direction.x > 0)
			{
				transform.localScale = new Vector3(multiplier, 1, 1);
			}
			else if (direction.x < 0)
			{
				transform.localScale = new Vector3(-1 * multiplier, 1, 1);
			}

		}

		public void SetDirection(Vector2 direction)
		{
			Direction = direction;
		}

		protected virtual float CalculateYVelocity()
		{
			float yVelocity = Direction.y * Speed;
			return yVelocity;
		}

		protected virtual float CalculateXVelocity()
		{
			float xVelocity = Direction.x * Speed;
			return xVelocity;
		}

		public virtual void TakeDamage()
		{

			Animator.SetTrigger(Hit);

		}
		[ContextMenu("Attack")]
		public virtual void Attack()
		{
			Animator.SetTrigger(AttackKey);
			Sounds.Play("Melee");
		}

		public void OnAttacking()
		{
			_attackRange.Check();

		}


	}
}

