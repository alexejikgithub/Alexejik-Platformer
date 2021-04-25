using System;
using UnityEngine;
namespace Platformer.Model.Definitions
{
	[CreateAssetMenu(menuName = "Defs/InventoryyItems", fileName = "InventoryItems")]
	public class InventoryItemsDef : ScriptableObject
	{
		[SerializeField] private ItemDef[] _items;

		public ItemDef Get(string id)
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
#if UNITY_EDITOR
		public ItemDef[] ItemsForEditor => _items;
#endif
	}


	[Serializable]
	public struct ItemDef
	{
		[SerializeField] private string _id;
		public string Id => _id;

		[SerializeField] private int _maxAmount;
		public int MaxAmount => _maxAmount;
		

		public bool IsVoid => string.IsNullOrEmpty(_id);
	}
}
