using System;
using System.Linq;
using UnityEngine;
namespace Platformer.Model.Definitions.Repositories.Items
{
	[CreateAssetMenu(menuName = "Defs/Items", fileName = "Items")]
	public class ItemsRepository : DefRepository<ItemDef>
	{
		[SerializeField] private ItemDef[] _items;

		private void OnEnable()
		{
			_collection = _items;
		}

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
	public struct ItemDef : IHaveId
	{
		[SerializeField] private string _id;
		public string Id => _id;


		[SerializeField] private int _maxAmount;
		public int MaxAmount => _maxAmount;

		[SerializeField] private ItemTag[] _tags;
		public bool HasTag(ItemTag tag)
		{
			return _tags != null ? _tags.Contains(tag) : false;
		}

		[SerializeField] private Sprite _icon;
		public Sprite Icon => _icon;

		public bool IsVoid => string.IsNullOrEmpty(_id);
	}
}
