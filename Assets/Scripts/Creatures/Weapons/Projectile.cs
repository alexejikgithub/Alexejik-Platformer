using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.Creatures.Weapons
{

	public class Projectile : BaseProjectile
	{


		protected override void Start()
		{
			base.Start();
			var force = new Vector2(Direction * Speed, 0);
			Rigidbody.AddForce(force, ForceMode2D.Impulse);
		}

		
	}
}