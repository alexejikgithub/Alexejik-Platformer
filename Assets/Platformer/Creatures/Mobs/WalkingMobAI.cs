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

	public class WalkingMobAI : BaseMobAI
	{



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
			var collider = GetComponent<CapsuleCollider2D>();
			collider.direction = CapsuleDirection2D.Horizontal;
			collider.offset = Vector2.zero;
			collider.size = new Vector2(0.78f, 0.47f);
		}
	}
}