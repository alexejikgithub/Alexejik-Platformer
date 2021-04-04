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

		private bool _invincibilityFrames;
		private void Awake()
		{
			_invincibilityFrames = false;
		}
		public void ApplyDamage(int damageValue)
		{
			if (!_invincibilityFrames)
			{
				_health -= damageValue;
				_onDamage?.Invoke();
				if (_health <= 0)
				{
					_onDie?.Invoke();
				}
			}

		}

		public void ApplyHealing(int healingValue)
		{
			_health += healingValue;
			_onHealing?.Invoke();

		}

		public void InvincibilityFramesTrue()
		{
			_invincibilityFrames = true;
		}

		public void InvincibilityFramesFalse()
		{
			_invincibilityFrames = false;
		}

	}

}
