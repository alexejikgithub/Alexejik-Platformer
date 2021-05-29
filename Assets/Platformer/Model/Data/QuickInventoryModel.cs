using Platformer.Model.Data.Properties;
using Platformer.Model.Definitions;
using Platformer.Utils.Disposables;
using System;
using System.Collections;
using UnityEngine;

namespace
	Platformer.Model.Data
{
	public class QuickInventoryModel: IDisposable
	{
		private readonly PlayerData _data;

		public InventoryItemData[] Inventory { get; private set; }

		public readonly IntProperty SelectedIndex = new IntProperty();

		public event Action OnChanged;

		public InventoryItemData SelectedItem => Inventory.Length >0 ? Inventory[SelectedIndex.Value]:null; 

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
			

			Inventory = _data.Inventory.GetAll(ItemTag.Usable);
			
			SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
			OnChanged?.Invoke();


		}

		internal void SetNextItem()
		{
			SelectedIndex.Value = (int)Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
		}

		public void Dispose()
		{
			_data.Inventory.OnChanged -= OnInventoryChanged;
		}
	}
}