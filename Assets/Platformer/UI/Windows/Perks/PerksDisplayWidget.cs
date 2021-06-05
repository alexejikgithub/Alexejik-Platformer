using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Repositories;
using Platformer.UI.Widgets;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Windows.Perks
{
	class PerksDisplayWidget : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private GameObject _isLocked;

		private GameSession _session;
		public string UsedPerk => _session.Data.Perks.Used.Value;


		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
			UpdateView();
		}

		[ContextMenu("UpdateView")]
		public void UpdateView()
		{

			var def = DefsFacade.I.Perks.Get(UsedPerk);
			if (def.Icon!=null)
			{
				_icon.sprite = def.Icon;
				_icon.color = Color.white;
			}
		}
	}
}
