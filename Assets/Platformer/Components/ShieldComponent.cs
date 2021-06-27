using Platformer.Components.Health;
using Platformer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Components
{
	public class ShieldComponent : MonoBehaviour
	{
		[SerializeField] private HealthComponent _health;
		[SerializeField] private Cooldown _cooldown; 

		public void Use()
		{
			_health.IsInvinsible = true;
			_cooldown.Reset();
			gameObject.SetActive(true);

		}

		private void Update()
		{
			if(_cooldown.IsReady)
			{
				gameObject.SetActive(false);
			}
		}
		private void OnDisable()
		{
			_health.IsInvinsible = false;
		}
	}
}
