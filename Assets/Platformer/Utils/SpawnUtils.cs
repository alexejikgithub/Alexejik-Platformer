﻿using System.Collections;
using UnityEngine;

namespace Platformer.Utils
{
	public class SpawnUtils : MonoBehaviour
	{

		private const string ContainerName = "###SPAWNED###";

		public static GameObject Spawn(GameObject prefab, Vector3 position , string containerName = ContainerName)
		{
			var container = GameObject.Find(containerName);
			if(container==null)
			{
				container = new GameObject(containerName);
			}

			return Object.Instantiate(prefab, position, Quaternion.identity, container.transform);
		}
	}
}