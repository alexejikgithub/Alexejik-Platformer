using Platformer.Model.Data.Properties;
using Platformer.Model.Definitions;
using Platformer.Utils.Disposables;
using System;
using System.Collections;
using UnityEngine;

namespace
	Platformer.Model.Data
{
	public class QuickInventoryModel
	{
		private readonly PlayerData _data;

		public InventoryItemData[] Inventory { get; private set; }

		public readonly IntProperty SelectedIndex = new IntProperty();

		public event Action OnChanged;

		public IDisposable Subscribe(Action call)
		{
			OnChanged += call;
			return new ActionDisposable(() => OnChanged -= call);
		}
		public QuickInventoryModel(PlayerData data)
		{
			
			_data = data;
			
			Inventory = _data.Inventory.GetAll(ItemTag.Usable);
			
			_data.Inventory.OnChanged += OnInventoryChanged;
		}

		private void OnInventoryChanged(string id, int value)
		{
			var indexFound = Array.FindIndex(Inventory, x => x.Id == id);
			if (indexFound != -1)
			{
				Inventory = _data.Inventory.GetAll();
				SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
				OnChanged?.Invoke();
			}


		}
	}
}