using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Components.Interactions
{
	public class InteractableComponent : MonoBehaviour
	{
		[SerializeField] private UnityEvent _action;


		public void Intract()
		{
			_action?.Invoke();
		}
	}
}

