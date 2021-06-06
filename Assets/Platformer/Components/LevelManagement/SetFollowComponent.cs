using Cinemachine;
using Platformer.Creatures.Hero;
using System.Collections;
using UnityEngine;

namespace Platformer.Components.LevelManagement
{
	[RequireComponent(typeof(CinemachineVirtualCamera))]
	public class SetFollowComponent : MonoBehaviour
	{

		private void Start()
		{
			var vCamera = GetComponent<CinemachineVirtualCamera>();
			var hero = FindObjectOfType<Hero>();
			if (hero != null)
			{
				vCamera.Follow = hero.transform;
			}

		}
	}
}