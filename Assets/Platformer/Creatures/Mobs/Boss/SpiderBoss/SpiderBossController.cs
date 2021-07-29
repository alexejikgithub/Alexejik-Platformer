using Platformer.Components.GoBased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Creatures.Mobs.Boss.SpiderBoss
{
	public class SpiderBossController : MonoBehaviour
	{
		[SerializeField] private EndlessMovementDown[] _movingObjects;

		public void StartMovement()
		{
			foreach (var item in _movingObjects)
			{
				item.Move();
			}
		}
		public void StopMovement()
		{
			foreach (var item in _movingObjects)
			{
				item.Stop();
			}
		}
	}
}
