using System;
using System.Collections;
using UnityEngine;

namespace Platformer.Components.ColliderBased
{
	[RequireComponent(typeof(Collider2D))]
	public class TmpDisableComponent : MonoBehaviour
	{
		[SerializeField] private float _disableTime=0.5f;
		private Collider2D _collider;
		private Coroutine _corutine;

		private void Awake()
		{
			_collider = GetComponent<Collider2D>();
		}
		private void OnDisable()
		{
			if (_corutine != null)
			{
				StopCoroutine(_corutine);
			}
		}

		public void DisableCollider()
		{
			if (_corutine != null)
			{
				StopCoroutine(_corutine);
			}
			_corutine = StartCoroutine(DisableAndEnable());
		}

		private IEnumerator DisableAndEnable()
		{
			_collider.enabled = false;
			yield return new WaitForSeconds(_disableTime);
			_collider.enabled = true;
		}
	}
}
