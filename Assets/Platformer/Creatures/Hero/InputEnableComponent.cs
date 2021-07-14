using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Platformer.Creatures.Hero
{
	class InputEnableComponent : MonoBehaviour
	{
		private PlayerInput _input;

		private void Start()
		{
			_input = FindObjectOfType<PlayerInput>();
		}
		public void SetInput(bool IsEnabled)
		{
			_input.enabled = IsEnabled;
		}
	}
}
