using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Utils
{
	public class DoActionOnAwake : MonoBehaviour
	{
		[SerializeField] private UnityEvent _doAction;

		private void Awake()
		{
			_doAction.Invoke();
		}

	}
}
