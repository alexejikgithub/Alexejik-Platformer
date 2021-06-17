using Platformer.Model;
using Platformer.Model.Data;
using Platformer.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.HUD
{
	public class CoinCounterWidget : MonoBehaviour
	{
		[SerializeField] private Text _value;

		private GameSession _session;

		private readonly PlayerData _data;

		private void Start()
		{
			OnLoad();
		}

		public void OnLoad()
		{
			_session = FindObjectOfType<GameSession>();
			UpdateAmount("Coin",0);
			_session.Data.Inventory.OnChanged -= UpdateAmount;
			_session.Data.Inventory.OnChanged += UpdateAmount;


		}

		public void UpdateAmount(string id, int value)
		{
			var coinAmount = _session.Data.Inventory.Count("Coin");
			_value.text = coinAmount.ToString();
		}


		private void OnDestroy()
		{
			_session.Data.Inventory.OnChanged -= UpdateAmount;
		}

	}
}
