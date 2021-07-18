using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Weapons
{
	public class BaseProjectile : MonoBehaviour
	{
		[SerializeField] protected float Speed;
		[SerializeField] protected bool _invertX;

        protected Rigidbody2D Rigidbody;
        protected int Direction;

        protected virtual void Start()
        {
            var mod = _invertX ? -1 : 1;
            Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
            Rigidbody = GetComponent<Rigidbody2D>();
        }


    }
}