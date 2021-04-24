using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Platformer.Components.ColliderBased
{
	public class LayerCheckForLanding : MonoBehaviour
	{
		[SerializeField] private LayerMask _groundLayer;

		[SerializeField] private UnityEvent _landingDust;


		private Collider2D _collider;



		private void Awake()
		{
			_collider = GetComponent<Collider2D>();
		}


		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (_collider.IsTouchingLayers(_groundLayer))
			{
				_landingDust?.Invoke();


			}

		}
	}
}