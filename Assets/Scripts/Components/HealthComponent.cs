using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Components
{
	public class HealthComponent : MonoBehaviour
	{
		[Range(0, 100)]
		[SerializeField] private int _health;
		[SerializeField] private UnityEvent _onDamage;
		[SerializeField] private UnityEvent _onHealing;
		[SerializeField] private UnityEvent _onDie;

		[SerializeField] private HealthChangeEvent _onChange;

		private bool _isDead = false;
		// _isDead will prevent invoking other events when target is already performing _onDie

		public void ApplyDamage(int damageValue)
		{
			if(!_isDead)
			{
				_health -= damageValue;
				_onChange?.Invoke(_health);
				_onDamage?.Invoke();
				if (_health <= 0)
				{
					_isDead = true;
					_onDie?.Invoke();
				}
			}
			
		}

		public void ApplyHealing(int healingValue)
		{
			_health += healingValue;
			_onChange?.Invoke(_health);
			_onHealing?.Invoke();

		}

#if UNITY_EDITOR
		[ContextMenu("Update Health")]


		private void UpdateHealth()
		{
			_onChange?.Invoke(_health);
		}
#endif


		internal void SetHealth(int health)
		{
			_health = health;
		}


		[Serializable]
		public class HealthChangeEvent : UnityEvent<int>
		{

		}
	}

}
