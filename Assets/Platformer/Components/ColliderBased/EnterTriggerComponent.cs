using Platformer.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Components.ColliderBased
{
	public class EnterTriggerComponent : MonoBehaviour
	{
		[SerializeField] private string _tag;
		[SerializeField] private LayerMask _layer = ~0;
		[SerializeField] private EnterEvent _enterAction;
		[SerializeField] private EnterEvent _exitAction;
		[SerializeField] private EnterEvent _stay2DAction;


		private void OnTriggerEnter2D(Collider2D other)
		{

			if (!other.gameObject.IsInLayer(_layer)) return;
			
			if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;
			
			_enterAction?.Invoke(other.gameObject);
			
		}

		private void OnTriggerExit2D(Collider2D other)
		{

			if (!other.gameObject.IsInLayer(_layer)) return;

			if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;

			_exitAction?.Invoke(other.gameObject);
			
			
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			if (!other.gameObject.IsInLayer(_layer)) return;

			if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;

			_stay2DAction?.Invoke(other.gameObject);

			
		}
		[Serializable]
		public class EnterEvent : UnityEvent<GameObject>
		{

		}
	}


}

