using Platformer.Model;
using Platformer.Model.Definitions.Repositories;
using Platformer.UI.Widgets;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Windows.Perks
{
	public class PerkWidget : MonoBehaviour, IItemRenderer<PerkDef>
	{
		[SerializeField] private Image _icon;
		[SerializeField] private GameObject _isUsed;
		[SerializeField] private GameObject _isSelected;
		[SerializeField] private GameObject _isLocked;

		private GameSession _session;
		private PerkDef _data;

		private void Start()
		{
			_session = GameSession.Instance;
			UpdateView();
		}

		public void SetData(PerkDef data, int index)
		{
			_data = data;
			if (_session != null)
			{
				UpdateView();
			}
		}

		private void UpdateView()
		{
			_icon.sprite = _data.Icon;
			_isUsed.SetActive(_session.PerksModel.IsUsed(_data.Id));
			_isSelected.SetActive(_session.PerksModel.InterfaceSelection.Value == _data.Id);
			_isLocked.SetActive(!_session.PerksModel.IsUnlocked(_data.Id));
		}

		private void OnSelect()
		{
			_session.PerksModel.InterfaceSelection.Value = _data.Id;
		}


	}
}
