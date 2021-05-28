using System.Collections;
using UnityEngine;
using Platformer.UI.Widgets;
using UnityEngine.UI;
using Assets.Platformer.Model.Definitions.Localization;
using UnityEngine.Events;
using System;

namespace Platformer.UI.Windows.LocalizationWindow
{
	public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
	{
		[SerializeField] private Text _text;
		[SerializeField] private GameObject _selector;
		[SerializeField] private SelectLocale _onSelected;

		private LocaleInfo _data;

		private void Start()
		{
			LocalizationManager.I.OnLoclaeChanged += UpdateSelection;
		}

		private void UpdateSelection()
		{
			var isSeleclet = LocalizationManager.I.LocaleKey == _data.LocaleId;
			_selector.SetActive(isSeleclet);
		}

		public void SetData(LocaleInfo localeInfo, int index)
		{
			_data = localeInfo;
			UpdateSelection();
			_text.text = localeInfo.LocaleId.ToUpper();
		}
		public void OnSelected()
		{
			_onSelected?.Invoke(_data.LocaleId);
		}
		private void OnDestroy()
		{
			LocalizationManager.I.OnLoclaeChanged -= UpdateSelection;
		}
	}

	[Serializable]
	public class SelectLocale : UnityEvent<string>
	{ 
	}

	public class LocaleInfo
	{
		public string LocaleId;

	}
}