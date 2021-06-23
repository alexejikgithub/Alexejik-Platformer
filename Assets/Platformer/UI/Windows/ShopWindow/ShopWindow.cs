using Platformer.Model;
using Platformer.Model.Data;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Repositories.Items;
using Platformer.Model.Models;
using Platformer.UI.Widgets;
using Platformer.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Windows.ShopWindow
{
	public class ShopWindow : AnimatedWindow
	{
		[SerializeField] private Transform _itemContainer;
		[SerializeField] private ShopItemWidget _prefab;


		[SerializeField] private Button _buyButton;
		[SerializeField] private Text _description;
		public ShopData ShopData{ get; set; }
		
		
		private ItemsRepository _itemRep;


		public int SelectedItem
		{
			get
			{
				return ShopData.SelectedItem;
			}
			set
			{
				ShopData.SelectedItem = value;
				InfoChanged();
			}
			
		}

		private DataGroup<ShopItemData, ShopItemWidget> _dataGroup;

		private GameSession _session;
		private ShopModel _shopModel;
		private readonly CompositeDisposable _trash = new CompositeDisposable();

		protected override void Start()
		{
			base.Start();

			//_dataGroup = new DataGroup<ShopItemData, ShopItemWidget>(_prefab, _itemContainer);

			_session = FindObjectOfType<GameSession>();
			_shopModel = _session.ShopModel;
			_shopModel.ShopWindowIsOpen = true;



		}

		public void InfoChanged()
		{

			var items = ShopData.ShopList;
			if(_dataGroup==null)
			{
				_dataGroup = new DataGroup<ShopItemData, ShopItemWidget>(_prefab, _itemContainer);
			}
			_dataGroup.SetData(items);
			var selected = ShopData.SelectedItem;
			var selectedItem = ShopData.GetItem(ShopData.ShopList[selected].Id);
			_buyButton.gameObject.SetActive(selectedItem.Price.Count != 0);

			if (_itemRep == null)
			{
				_itemRep = DefsFacade.I.Items;
			}
			_description.text = _itemRep.Get(selectedItem.Id).Description;



		}

		public void SetShopData(ShopData shopData)
		{
			ShopData = shopData;
			InfoChanged();
		}

		protected override void Close()
		{
			
			_shopModel.ShopWindowIsOpen = false;

			base.Close();
			
		}

		public void BuyButton()
		{
			var model = _session.ShopModel;
			var selected = ShopData.SelectedItem;
			var selectedItem = ShopData.GetItem(ShopData.ShopList[selected].Id);
			model.Buy(selectedItem);
		}

	}
}
