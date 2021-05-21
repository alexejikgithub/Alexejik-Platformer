using Platformer.Model.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Model.Data
{
	[Serializable]
	public class InventoryData
	{
		[SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();
		public List<InventoryItemData> Inventory => _inventory;


		public delegate void OnInventoryChanged(string id, int value);
		public OnInventoryChanged OnChanged;
		private InventoryItemData item;
		private bool isFull;
		private int count;
		private ItemDef itemDef;

		public void Add(string id, int value)
		{
			if (value <= 0) return;
			itemDef = DefsFacade.I.Items.Get(id);
			if (itemDef.IsVoid) return;

			isFull = _inventory.Count >= DefsFacade.I.Player.InventorySize;
			item = GetItem(id);
			count = Count(id); // Current amount will be compared to maximum amount. 



			

			if (itemDef.HasTag(ItemTag.Stackable) && count >= itemDef.MaxAmount) return; // If current amount== maximum amount and stackable, nothing will be added.

			if (itemDef.HasTag(ItemTag.Stackable))
			{
				AddToStack(id, value);
			}

			if (!itemDef.HasTag(ItemTag.Stackable))
			{
				
				AddNonStack(id, value);
			}



			OnChanged?.Invoke(id, Count(id));
		}


		public InventoryItemData[] GetAll(params ItemTag[] tags)
		{
			
			var retValue = new List<InventoryItemData>();
			foreach (var item in _inventory)
			{
				var itemDef = DefsFacade.I.Items.Get(item.Id);
				
				var isAllRequirementsMet = tags.All(x => itemDef.HasTag(x));
				
				if (isAllRequirementsMet)
				{
					retValue.Add(item);
				}
			}
			
			return retValue.ToArray();
		}

		private void AddToStack(string id, int value)
		{
			if (item == null)
			{
				if (isFull) return;
				item = new InventoryItemData(id);
				_inventory.Add(item);
				
			}
			item.Value += (itemDef.MaxAmount - count) > value ? value : (itemDef.MaxAmount - count); // will add the required amount to counter. If value is higher than maximum amount, it will add up to maximum.v 
		}

		private void AddNonStack(string id, int value)
		{
			
			var itemLasts = DefsFacade.I.Player.InventorySize - _inventory.Count;
			value = Mathf.Min(itemLasts, value);

			
			for (int i = 0; i < value; i++)
			{

				item = new InventoryItemData(id) { Value = 1 };
				_inventory.Add(item);

			}
		}


		public void Remove(string id, int value)
		{
			var itemDef = DefsFacade.I.Items.Get(id);
			if (itemDef.IsVoid) return;

			var item = GetItem(id);
			if (item == null) return;

			item.Value -= value;

			if (item.Value <= 0)
			{
				_inventory.Remove(item);
			}

			OnChanged?.Invoke(id, Count(id));
		}

		public InventoryItemData GetItem(string id)
		{
			foreach (var itemData in _inventory)
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
			foreach (var itemData in _inventory)
			{
				if (itemData.Id == id)
				{
					count += itemData.Value;
				}
			}
			return count;

		}
	}

	[Serializable]
	public class InventoryItemData
	{
		[InventoryId] public string Id;
		public int Value;

		public InventoryItemData(string id)
		{
			Id = id;

		}
	}

}
