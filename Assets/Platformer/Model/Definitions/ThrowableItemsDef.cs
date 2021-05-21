using System;
using System.Collections;
using UnityEngine;

namespace Platformer.Model.Definitions
{

	[CreateAssetMenu(menuName = "Defs/ThrowableItemsDef", fileName = "ThrowableItemsDef")]
	public class ThrowableItemsDef : ScriptableObject
	{
		[SerializeField] private ThrowableDef[] _items;

		public ThrowableDef Get(string id)
		{
			foreach (var itemDef in _items)
			{
				if (itemDef.Id == id)
				{
					return itemDef;
				}
			}
			return default;
		}

		[Serializable]
		public struct ThrowableDef
		{
			[InventoryId] [SerializeField] private string _id;

			[SerializeField] private GameObject _projectile;

			
			public string Id  => _id;
			public GameObject Projectile => _projectile;
		}


	}
}