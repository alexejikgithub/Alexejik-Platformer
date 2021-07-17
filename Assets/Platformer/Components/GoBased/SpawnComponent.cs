using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Utils;
using Platformer.Model.Definitions;
using System;
using Platformer.Utils.ObjectPool;

namespace Platformer.Components.GoBased
{

    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] protected Transform _target;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] private bool _usePool;


        [ContextMenu ("Spawn")]
        public void Spawn()
		{
            SpawnInstance();

        }

        public virtual GameObject SpawnInstance()
		{
            var targetPosition = _target.position;
            var instantiate = _usePool
                ? Pool.Instance.Get(_prefab, targetPosition)
                : SpawnUtils.Spawn(_prefab, targetPosition);
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