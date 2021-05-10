using Platformer.Components.ColliderBased;
using Platformer.Creatures.Mobs.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Creatures.Mobs
{
	public class FlyingMobAI : BaseMobAI
	{
		[SerializeField] FindPathGrid _grid;

		public bool _agro=false;

		public void AgroOn()
		{
			_agro = true;
		}
		public void AgroOff()
		{
			_agro = false;
		}

		protected override Vector2 GetDirectionToTarget()

		{
			
			Vector2 direction;
			if(_grid.Path!=null)
			{
				 direction = _grid.Path[0].WorldPosition - transform.position;
			}
			else
			{
				direction = transform.position;
			}
			return direction.normalized;
		}

		protected override IEnumerator GoToHero()
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
		protected override void ChangeOnDeath()
		{
			var rb = GetComponent<Rigidbody2D>();
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.gravityScale = 50;
		}
	}
}