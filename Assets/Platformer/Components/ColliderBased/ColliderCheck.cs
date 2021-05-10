using UnityEngine;


namespace Platformer.Components.ColliderBased
{

	public class ColliderCheck : LayerCheck
	{

		private Collider2D _collider;

		private void Awake()
		{
			_collider = GetComponent<Collider2D>();
		}


		private void OnTriggerStay2D(Collider2D collision)
		{
			_isTouchingLayer = (_collider.IsTouchingLayers(Layer));

		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			_isTouchingLayer = (_collider.IsTouchingLayers(Layer));

		}


	}
}