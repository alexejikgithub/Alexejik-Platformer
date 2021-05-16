using Platformer.Components.Health;
using Platformer.UI.Widgets;
using System.Collections;
using UnityEngine;

namespace Platformer.UI.HUD
{
	public class PersonalBarController : MonoBehaviour
	{

		[SerializeField] private ProgressBarWidget _healthBar;
		[SerializeField] private HealthComponent _healthComponent;

		private int _maxHealth;
		
		private void Start()
		{
			_maxHealth = _healthComponent.Health;
			_healthBar.SetProgress(1);
		}


		public void UpdateHealthBar(int newValue)
		{
			var value = (float)newValue / _maxHealth;
			_healthBar.SetProgress(value);
		}




	}
}