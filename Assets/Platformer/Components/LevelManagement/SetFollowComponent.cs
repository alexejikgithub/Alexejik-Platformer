using Cinemachine;
using Platformer.Creatures.Hero;
using System.Collections;
using UnityEngine;

namespace Platformer.Components.LevelManagement
{
	[RequireComponent(typeof(CinemachineVirtualCamera))]
	public class SetFollowComponent : MonoBehaviour
	{
		private CinemachineVirtualCamera _vCamera;
		private Hero _hero;
		private void Start()
		{
			_vCamera = GetComponent<CinemachineVirtualCamera>();
			_hero = FindObjectOfType<Hero>();
			SetFollowToHero();

		}


		public void SetFollow(GameObject obj)
		{
			Debug.Log(obj);
			if(obj!=null)
			{
				_vCamera.Follow = obj.transform;
			}

		}
		public void SetFollowToHero()
		{
			SetFollow(_hero.gameObject);
		}

	}
}