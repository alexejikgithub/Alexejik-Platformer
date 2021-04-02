using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer
{
	public class HeroInputReader : MonoBehaviour
	{
		private Hero _hero;



		private void Awake()
		{
			_hero = GetComponent<Hero>();

		}



		public void OnHorizontalMovement(InputAction.CallbackContext context)
		{
			var direction = context.ReadValue<Vector2>();
			_hero.SetDirection(direction);


		}

		public void OnSaySomething(InputAction.CallbackContext context)
		{

			if (context.canceled)
			{
				_hero.SaySomehting();
			}


		}

		public void OnInteract(InputAction.CallbackContext context)
		{

			if (context.canceled)
			{
				_hero.Interact();

			}
		}

		public void OnSprint(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				_hero.SetIsSprinting(true);
			}

			if (context.canceled)
			{
				_hero.SetIsSprinting(false);

			}

		}

		public void jumpingOffTheWall(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				Debug.Log("!!!");
				_hero.DoJumpOffTheWall();
			}
		}

	}

}

