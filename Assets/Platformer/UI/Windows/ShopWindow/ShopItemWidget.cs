using Assets.Platformer.Model.Definitions.Localization;
using Platformer.Model;
using Platformer.Model.Data;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Repositories.Items;
using Platformer.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Windows.ShopWindow
{
	public class ShopItemWidget :MonoBehaviour, IItemRenderer<ShopItemData>
	{
		[SerializeField] private Text _name;
		[SerializeField] private Image _icon;
		[SerializeField] private Text _currentAmount;
		[SerializeField] private GameObject _selector;
		[SerializeField] private ItemWidget _price;

		private int _index;
		private ShopItemData _data;
		private GameSession _session;
		private ItemsRepository _itemRep;
		private ShopWindow _window;
		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
			_itemRep = DefsFacade.I.Items;
			_window = FindObjectOfType<ShopWindow>();
			UpdateView();
		}

		public void SetData(ShopItemData data, int index)
		{
			_data = data;
			_index = index;
			if (_session != null)
			{
				UpdateView();
			}
		}

		



		

		[ContextMenu("update")]
		private void UpdateView()
		{
			var shopModel = _session.ShopModel;
			_icon.sprite = _itemRep.Get(_data.Id).Icon;
			_name.text = LocalizationManager.I.Localize(_itemRep.Get(_data.Id).Id);
			_currentAmount.text = _data.Count.ToString();
			_price.SetData(_data.Price);

			_selector.SetActive(_window.SelectedItem == _index);
		}

		public void OnSelect()
		{
			_window.SelectedItem = _index;
			
		}
		
		public string GetDescription()
		{
			return _itemRep.Get(_data.Id).Description;
		}
	}
}
