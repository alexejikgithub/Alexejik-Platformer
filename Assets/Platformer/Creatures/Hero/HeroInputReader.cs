using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Platformer.Creatures;

namespace Platformer.Creatures.Hero
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

		

		public void OnInteract(InputAction.CallbackContext context)
		{

			if (context.canceled)
			{
				_hero.Interact();

			}
		}

		/*
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
		*/

		public void OnAttack(InputAction.CallbackContext context)
		{

			if(context.started)
			{

			}
			if (context.canceled)
			{
				_hero.Attack();

			}
		}

		public void OnThrow(InputAction.CallbackContext context)
		{

			if (context.canceled)
			{
				_hero.Throw();

			}
		}

		public void OnThrowBunch(InputAction.CallbackContext context)
		{

			if (context.performed)
			{
				StartCoroutine(_hero.ThrowBunch());

			}
		}

	}

}

