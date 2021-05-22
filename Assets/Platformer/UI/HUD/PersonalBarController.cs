using Platformer.Components.Health;
using Platformer.Creatures;
using Platformer.UI.Widgets;
using System.Collections;
using UnityEngine;

namespace Platformer.UI.HUD
{
	public class PersonalBarController : MonoBehaviour
	{

		[SerializeField] private ProgressBarWidget _healthBar;
		[SerializeField] private HealthComponent _healthComponent;

		private Creature _creature;

		private int _maxHealth;
		
		private void Start()
		{
			_maxHealth = _healthComponent.Health;
			_healthBar.SetProgress(1);
			_creature = _healthComponent.gameObject.GetComponent<Creature>();
			_creature.OnChangedDirection += OnDirectionChanged;
		}

		public void Dispose()
		{
			_creature.OnChangedDirection -= OnDirectionChanged;
		}


			public void UpdateHealthBar(int newValue)
		{
			var value = (float)newValue / _maxHealth;
			_healthBar.SetProgress(value);
		}
		private void OnDirectionChanged (Vector2 direction)
		{
			
			if (direction.x > 0)
			{
				transform.localScale = new Vector3(-1, 1, 1);
			}
			else if (direction.x < 0)
			{
				transform.localScale = new Vector3(1 , 1, 1);
			}
		}

		





	}
}