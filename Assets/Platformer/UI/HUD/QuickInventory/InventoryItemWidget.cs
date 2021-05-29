﻿using Platformer.Model;
using Platformer.Model.Data;
using Platformer.Model.Definitions;
using Platformer.Utils.Disposables;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.HUD.QuickInventory
{
	public class InventoryItemWidget : MonoBehaviour
	{

		[SerializeField] private Image _icon;
		[SerializeField] private GameObject _selection;
		[SerializeField] private Text _value;

		private readonly CompositeDisposable _trash = new CompositeDisposable();

		private int _index;

		private void Start()
		{
			var session = FindObjectOfType<GameSession>();
			var index = session.QuickInventory.SelectedIndex;

			_trash.Retain(index.SubscribeAndInvoke(OnIndexChanged));
		}

		private void OnIndexChanged(int newValue, int oldValue)
		{
			_selection.SetActive(_index == newValue);
		}

		public void SetData(InventoryItemData item, int index)
		{
			_index = index;
			var def = DefsFacade.I.Items.Get(item.Id);
			_icon.sprite = def.Icon;
			_value.text = def.HasTag(ItemTag.Stackable) ? item.Value.ToString() : string.Empty;
		}

		private void OnDestroy()
		{
			_trash.Dispose();
		}
	}
}