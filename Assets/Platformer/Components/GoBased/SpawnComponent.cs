using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Utils;
using Platformer.Model.Definitions;
using System;

namespace Platformer.Components.GoBased
{

    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;


        [ContextMenu ("Spawn")]
        public void Spawn()
		{
            SpawnInstance();

        }

        public GameObject SpawnInstance()
		{
            var instantiate = SpawnUtils.Spawn(_prefab, _target.position);
            instantiate.transform.localScale = _target.lossyScale;
            instantiate.SetActive(true);
            return instantiate;
        }

		internal void SetPrefab(GameObject prefab)
		{
            _prefab = prefab;
		}
	}
}