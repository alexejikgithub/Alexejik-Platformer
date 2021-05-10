using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Platformer.Components.ColliderBased
{
	public class EnterCollisionComponent : MonoBehaviour
	{
		[SerializeField] private string[] _tags;
		[SerializeField] private EnterEvent _action;


		private void OnCollisionEnter2D(Collision2D other)
		{
			foreach (var tag in _tags)
			{
				if (other.gameObject.CompareTag(tag))
				{

					_action?.Invoke(other.gameObject);


				}
			}


		}
		[Serializable]
		public class EnterEvent : UnityEvent<GameObject>
		{

		}
	}
}