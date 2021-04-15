using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components
{


	public class SpawnCoinsComponent : MonoBehaviour
	{
		[SerializeField] private Transform _target;
		[SerializeField] private GameObject[] _arrayOfPrefabs;
		[SerializeField] private int _numberOfSpawns;

		[ContextMenu("Spawn")]
		public void Spawn()
		{
			for(int i =0; i< _numberOfSpawns;i++)
			{
				int prefabIndex = Random.Range(0, _arrayOfPrefabs.Length);

				var instantiate = Instantiate(_arrayOfPrefabs[prefabIndex], _target.position, Quaternion.identity);
				instantiate.transform.localScale = _target.lossyScale;
			}

			

		}



	}
}