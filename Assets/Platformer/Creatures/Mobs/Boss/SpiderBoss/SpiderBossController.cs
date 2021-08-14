using Platformer.Components.GoBased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinding;
using Platformer.Components;
using Platformer.Components.LevelManagement;

namespace Platformer.Creatures.Mobs.Boss.SpiderBoss
{
	public class SpiderBossController : MonoBehaviour
	{
		[SerializeField] private EndlessMovementDown[] _movingObjects;
		[SerializeField] private GameObject[] _objectsForStage3;
		[SerializeField] private GameObject _enterDoorTiles;
		[SerializeField] private GameObject _exitDoorTiles;
		[SerializeField] private GameObject _exitTrigger;
		[SerializeField] private GameObject _groundTiles;
		[SerializeField] private GameObject _rockSpawner;
		[SerializeField] private GameObject _aStar;
		[SerializeField] private GameObject _fixedCameraPoint;
		[SerializeField] private SetFollowComponent _camera;
		[SerializeField] private AliveSpiderCounter _spiders;
		[SerializeField] private Animator _animator;
		[SerializeField] private SemiCircularProjectileSpawner _projectileSpawner;
		




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
			_camera.SetFollow(_fixedCameraPoint);
			_enterDoorTiles.SetActive(true);
			_groundTiles.SetActive(false);
		}
		public void InitiateStage2()
		{
			_rockSpawner.SetActive(true);
		}
		public void InitiateStage3()
		{
			StopMovement();
			_camera.SetFollowToHero();
			_rockSpawner.SetActive(false);
			foreach (var item in _objectsForStage3)
			{
				item.SetActive(true);
			}
			_aStar.GetComponent<NavGraph>().Scan();

		}
		public void InitiateStage4()
		{
			_exitDoorTiles.SetActive(false);
			_exitTrigger.SetActive(true);
		}	
		public void UpdateSmallSpidersAmount()
		{
			_animator.SetBool("haveSpiders", _spiders.HaveChildren);
		}
		public void SpawnProjectiles()
		{
			_projectileSpawner.LaunchProjectiles();
		}
	}
}
