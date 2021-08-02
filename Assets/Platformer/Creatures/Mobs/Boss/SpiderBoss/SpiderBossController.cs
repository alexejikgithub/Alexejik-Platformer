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
		[SerializeField] private GameObject _enterDoorTiles;
		[SerializeField] private GameObject _groundTiles;
		[SerializeField] private GameObject _rockSpawner;
		



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

		public void InitiateStage1()
		{
			StartMovement();
			_enterDoorTiles.SetActive(true);
			_groundTiles.SetActive(false);
		}
		public void InitiateStage2()
		{
			_rockSpawner.SetActive(true);
		}

	}
}
