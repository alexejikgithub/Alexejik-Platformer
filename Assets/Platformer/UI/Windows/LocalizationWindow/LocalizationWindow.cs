using Assets.Platformer.Model.Definitions.Localization;
using Platformer.UI.Widgets;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.UI.Windows.LocalizationWindow
{
	public class LocalizationWindow : AnimatedWindow
	{

		[SerializeField] private Transform _container;
		[SerializeField] private LocaleItemWidget _prefab;

		private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;

		private string[] _supportedLocales = { "en", "ru", "ch" };
		protected override void Start()
		{
			base.Start();
			_dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);
			_dataGroup.SetData(CompeseData());

			

		}
		

		private List<LocaleInfo> CompeseData()
		{
			var data = new List<LocaleInfo>();
			foreach (var locale in _supportedLocales)
			{
				data.Add(new LocaleInfo { LocaleId = locale });
			}
			return data;
		}

		public void OnSelected(string selectedLocale)
		{
			LocalizationManager.I.SetLocale(selectedLocale);
		}

	}
}