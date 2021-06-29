using Platformer.Model.Data;
using Platformer.Model.Data.Properties;
using Platformer.UI.Windows.ShopWindow;
using Platformer.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Model.Models
{
	public class ShopModel :  IDisposable

	{
		private readonly PlayerData _playerData;
		public event Action OnChanged;
		private ShopWindow _shopWindow;
		private ShopData _shopData;
		public bool ShopWindowIsOpen { get; set; }
		


		public ShopModel(PlayerData data)
		{
			_playerData = data;
			ShopWindowIsOpen = false;
		}


		internal bool CanBuy(ShopItemData itemData)
		{
			return _playerData.Inventory.IsEnough(itemData.Price);

		}
		public void Buy(ShopItemData itemData)
		{

			if (!CanBuy(itemData)) return;
			//if (_shopData == null)
			//{
				_shopWindow =  GameObject.FindObjectOfType<ShopWindow>();
				_shopData = _shopWindow.ShopData;
			//}
			if (_shopData.Count(itemData.Id) <= 0) return;
			Debug.Log(_shopData.Count(itemData.Id));
			_shopData.GetItem(itemData.Id).Count -= 1;
			_playerData.Inventory.Remove(itemData.Price.ItemId, itemData.Price.Count);
			_playerData.Inventory.Add(itemData.Id, 1);
			_shopWindow.InfoChanged();


		}

		public void Dispose()
		{

		}

	}
}
