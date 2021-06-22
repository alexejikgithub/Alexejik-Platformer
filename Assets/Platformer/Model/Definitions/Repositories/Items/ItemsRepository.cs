using System;
using System.Linq;
using UnityEngine;
namespace Platformer.Model.Definitions.Repositories.Items
{
	[CreateAssetMenu(menuName = "Defs/Items", fileName = "Items")]
	public class ItemsRepository : DefRepository<ItemDef>
	{
		
#if UNITY_EDITOR
		public ItemDef[] ItemsForEditor => _collection;
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
			return _tags?.Contains(tag) ?? false;
		}

		[SerializeField] private Sprite _icon;
		public Sprite Icon => _icon;

		public bool IsVoid => string.IsNullOrEmpty(_id);

		[SerializeField] private string _description;
		public string Description => _description;
	}
}
