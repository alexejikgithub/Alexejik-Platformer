using Platformer.Creatures.Weapons;
using Platformer.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Components.GoBased
{
	public class SemiCircularProjectileSpawner : CircularProjectileSpawner
	{
		protected override IEnumerator SpawnProjectiles()
		{
			var sequence = _settings[Stage];

			foreach (var settings in sequence.Sequence)
			{
				var sectorStep = 2 * Mathf.PI / settings.BurstCount;


				for (int i = 0, burstCount = 1; i < settings.BurstCount/2+1; i++, burstCount++)
				{

					var angle = sectorStep * i;

					var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
					var instance = SpawnUtils.Spawn(settings.Prefab.gameObject, transform.position);
					var projectile = instance.GetComponent<DirectionalProjectile>();
					projectile.Launch(direction);

					if (burstCount < settings.ItemPerBurst) continue;

					burstCount = 0;
					yield return new WaitForSeconds(settings.Delay);


				}

			}


		}
	}
}
