using Platformer.Model;
using Platformer.Model.Data;
using Platformer.Model.Models;
using Platformer.UI.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.UI.Windows.ShopWindow
{
	public class ShopController : MonoBehaviour
	{
		[SerializeField] ShopData _shopData;
		[SerializeField] private ShopWindow _prefab;
		private ShopWindow _window;
		private GameSession _session;
		private ShopModel _shopModel;
		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
			_shopModel = _session.ShopModel;
			
		}

		[ContextMenu("OpenWindow")]
		public void OpenShopWindow()
		{
			Debug.Log(_shopModel.ShopWindowIsOpen);
			if (_shopModel.ShopWindowIsOpen == false)
			{
				
				var canvas = FindObjectOfType<BaseCanvas>();

				_window = Instantiate(_prefab, canvas.transform);
				_window.SetShopData(_shopData);
				_window.InfoChanged();
			}

			
			

		}


	}
}
