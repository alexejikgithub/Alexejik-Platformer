using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Utils.ObjectPool
{
	public class Pool : MonoBehaviour
	{

		private readonly Dictionary<int, Queue<PoolItem>> _items = new Dictionary<int, Queue<PoolItem>>();

		private static Pool _instance;

		public static Pool Instance
		{
			get
			{
				if (_instance==null)
				{
					var go = new GameObject("###MAIN_POOL");
					_instance = go.AddComponent<Pool>();
				}
				return _instance;
			}
		}

		public GameObject Get(GameObject go, Vector3 position)
		{
			var id = go.GetInstanceID();
			var queue = RequiredQueue(id);

			if(queue.Count > 0)
			{
				var pooledItem = queue.Dequeue();
				pooledItem.transform.position = position;
				
				pooledItem.gameObject.SetActive(true);
				pooledItem.Restart();
				return pooledItem.gameObject;
			}
			var instance = SpawnUtils.Spawn(go, position, gameObject.name);
			var poolItem = instance.GetComponent<PoolItem>();
			poolItem.Retain(id, this);
			return instance;
			
		}

		private Queue<PoolItem> RequiredQueue(int id)
		{
			if (!_items.TryGetValue(id, out var queue))
			{
				queue = new Queue<PoolItem>();
				_items.Add(id, queue);
			}
			return queue;
		}

		public void Release(int id, PoolItem poolItem)
		{
			var queue = RequiredQueue(id);
			queue.Enqueue(poolItem);
			poolItem.gameObject.SetActive(false);
		}
	}
}
