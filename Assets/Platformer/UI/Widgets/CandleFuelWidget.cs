using Platformer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Widgets
{
	public class CandleFuelWidget : MonoBehaviour
	{

		[SerializeField] private Image _fillImage;

		private GameSession _session;

		private void Start()
		{
			OnLoad();
		}


		public void OnLoad()
		{
			_session = GameSession.Instance;
		}


		private void Update()
		{
			if(_session.Data.Inventory.GetItem("CandleFuel")!=null)
			{
				_fillImage.fillAmount = _session.Data.Inventory.GetItem("CandleFuel").Value / 100f;
			}
			
		}
	}
}
