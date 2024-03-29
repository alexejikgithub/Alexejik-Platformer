﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Components
{
	public class HealthComponent : MonoBehaviour
	{
		[Range (0,100)]
		[SerializeField] private int _health;
		[SerializeField] private UnityEvent _onDamage;
		[SerializeField] private UnityEvent _onHealing;
		[SerializeField] private UnityEvent _onDie;

		public void ApplyDamage(int damageValue)
		{
			_health -= damageValue;
			_onDamage?.Invoke();
			if(_health<=0)
			{
				_onDie?.Invoke();
			}
		}

		public void ApplyHealing(int healingValue)
		{
			_health += healingValue;
			_onHealing?.Invoke();
			
		}

	}

}
