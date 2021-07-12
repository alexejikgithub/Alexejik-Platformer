using Platformer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Components.GoBased
{
	public class RandomBombSpawner : SpawnComponent
	{
		[SerializeField] int xMin, xMax;
		public override GameObject SpawnInstance()
		{
			var randomX = new System.Random().Next(xMin, xMax);

			var instantiate = SpawnUtils.Spawn(_prefab, new Vector3(randomX, _target.position.y, _target.position.z));
			instantiate.SetActive(true);
			return instantiate;
		}
	}
}
