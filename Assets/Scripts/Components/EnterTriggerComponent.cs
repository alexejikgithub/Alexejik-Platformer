using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Components
{
	public class EnterTriggerComponent : MonoBehaviour
	{
		[SerializeField] private string _tag;
		[SerializeField] private EnterEvent _action;


		private void OnTriggerEnter2D(Collider2D other)
		{
			
			if (other.gameObject.CompareTag(_tag))
			{

				_action?.Invoke(other.gameObject);


			}

		}
		[Serializable]
		public class EnterEvent : UnityEvent<GameObject>
		{

		}
	}
	

}

