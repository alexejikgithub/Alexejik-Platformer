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

		public int HealthPointsToChange => _healthPointsToChange;

		[Header("Using Stats")]
		
		// [SerializeField] private StatId[] _usedStats;
		// private int _critDamageMultiplier = 1;
		GameSession _session;

		public bool _dealHealing => _healOrDamage == ChangingHealsStates.Heal;
		public bool _dealDamage => _healOrDamage == ChangingHealsStates.Damage;

		private void Start()
		{
			_session = GameSession.Instance;
		}

		private enum ChangingHealsStates
		{
			Heal,
			Damage
		}

		public void SetAmount(int amount)
		{
			_healthPointsToChange = amount;
		}


		public void ChangeHealthAmount(GameObject target)
		{
		
			
			//foreach (var stat in _usedStats)
			//{
				
			//	switch (stat)
			//	{
			//		case StatId.RangeDamage:
			//			_healthPointsToChange = (int)_session.StatsModel.GetValue(StatId.RangeDamage);
			//			break;
			//		case StatId.CritDamage:
			//			var randomInt = Random.Range(0, 100);
						


			//			if(randomInt<= (int)_session.StatsModel.GetValue(StatId.CritDamage))
			//			{
			//				_critDamageMultiplier = 2;

			//			}
			//			else
			//			{
			//				_critDamageMultiplier = 1;
			//			}
			//			break;
			//	}
			//}
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