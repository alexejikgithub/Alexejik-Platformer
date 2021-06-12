using Platformer.Model;
using Platformer.Model.Definitions.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components.Health
{
	public class ChangeHealthComponent : MonoBehaviour
	{
		[Range(0, 100)]
		[SerializeField] private int _healthPointsToChange;
		[SerializeField] private ChangingHealsStates _healOrDamage;

		[Header("Using Stats")] 
		[SerializeField] private bool _useHeroStats;
		[SerializeField] private StatId _usedStat;

		GameSession _session;

		public bool _dealHealing => _healOrDamage == ChangingHealsStates.Heal;
		public bool _dealDamage => _healOrDamage == ChangingHealsStates.Damage;

		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
		}

		private enum ChangingHealsStates
		{
			Heal,
			Damage
		}

		

		public void ChangeHealthAmount(GameObject target)
		{
			if(_useHeroStats)
			{
				_healthPointsToChange = (int) _session.StatsModel.GetValue(StatId.RangeDamage);
			}
			var healthComponent = target.GetComponent<HealthComponent>();
			if (healthComponent != null)
			{
				if (_dealDamage)
				{
					healthComponent.ApplyDamage(_healthPointsToChange);
				}
				else
				{
					healthComponent.ApplyHealing(_healthPointsToChange);
				}


			}
		}
	}
}