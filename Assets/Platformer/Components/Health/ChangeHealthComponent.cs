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
		// [SerializeField] private bool _useHeroStats;
		[SerializeField] private StatId[] _usedStats;
		private int _critDamageMultiplier = 1;
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
			//if(_useHeroStats)
			//{
			//	_healthPointsToChange = (int) _session.StatsModel.GetValue(StatId.RangeDamage);
			//}
			
			foreach (var stat in _usedStats)
			{
				
				switch (stat)
				{
					case StatId.RangeDamage:
						_healthPointsToChange = (int)_session.StatsModel.GetValue(StatId.RangeDamage);
						break;
					case StatId.CritDamage:
						var randomInt = Random.Range(0, 100);
						


						if(randomInt<= (int)_session.StatsModel.GetValue(StatId.CritDamage))
						{
							_critDamageMultiplier = 2;

						}
						else
						{
							_critDamageMultiplier = 1;
						}
						break;
				}
			}
			var healthComponent = target.GetComponent<HealthComponent>();
			if (healthComponent != null)
			{
				if (_dealDamage)
				{
					healthComponent.ApplyDamage(_healthPointsToChange * _critDamageMultiplier);
				}
				else
				{
					healthComponent.ApplyHealing(_healthPointsToChange);
				}


			}
		}
	}
}