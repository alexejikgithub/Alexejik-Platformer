using Platformer.Model.Data;
using Platformer.UI.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.UI.Windows.ShopWindow
{
	public class ShopController :MonoBehaviour
	{
		[SerializeField] ShopData _shopData;
		[SerializeField] private ShopWindow _prefab;
		private ShopWindow _window;

		[ContextMenu("OpenWindow")]
		public void OpenShopWindow ()
		{
			
			var canvas = FindObjectOfType<BaseCanvas>();

			_window = Instantiate(_prefab, canvas.transform);
			_window.SetShopData(_shopData);
			_window.InfoChanged();

		}
		

	}
}
