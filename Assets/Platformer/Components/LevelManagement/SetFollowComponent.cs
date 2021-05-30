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
			vCamera.Follow = FindObjectOfType<Hero>().transform;
		}
	}
}