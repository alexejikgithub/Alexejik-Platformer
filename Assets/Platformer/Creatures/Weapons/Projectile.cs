using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Creatures;


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

        public void Restart()
		{
            var mod = _invertX ? -1 : 1;
            var hero = FindObjectOfType<Hero.Hero>();  // Very bad idea. Need to change later
            Direction = mod * hero.transform.lossyScale.x > 0 ? 1 : -1;
            var force = new Vector2(Direction * Speed, 0);
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }


    }
}