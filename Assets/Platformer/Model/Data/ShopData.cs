using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Repositories;
using Platformer.Model.Definitions.Repositories.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer.Model.Data
{
	[Serializable]
	public class ShopData : MonoBehaviour
	{
		[SerializeField] private List<ShopItemData> _shopList = new List<ShopItemData>();
		public List<ShopItemData> ShopList => _shopList;
		


		private int _selectedItem;
		public delegate void OnSgopListChanged(string id, int count);
		public event OnSgopListChanged OnChanged;

		public int SelectedItem
		{
			get
			{
				return _selectedItem;
			}

			set
			{
				_selectedItem = value;
			}

		}


		private void Awake()
		{
			_selectedItem = 0;
		}

		// Removes item from _shopList
		public void Remove(string id, int count)
		{
			var itemDef = DefsFacade.I.Items.Get(id);
			if (itemDef.IsVoid)
			{
				return;
			}
			var item = GetItem(id);

			if (item == null)
			{
				return;
			}

			item.Count -= count;
			if (item.Count <= 0)
			{
				_shopList.Remove(item);
			}

			OnChanged?.Invoke(id, Count(id));
		}

		public ShopItemData GetItem(string id)
		{
			foreach (var itemData in _shopList)
			{
				if (itemData.Id == id)
				{
					return itemData;
				}
			}
			return null;
		}

		public int Count(string id)

		{
			var count = 0;
			foreach (var itemData in _shopList)
			{
				if (itemData.Id == id)
				{
					count += itemData.Count;
				}
			}
			return count;

		}
	}
	[Serializable]
	public class ShopItemData
	{
		[InventoryId] public string Id;
		public int Count;
		public ItemWithCount Price;

		public ShopItemData(string id)
		{
			Id = id;
		}
	}
}
