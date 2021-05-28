using Platformer.Model.Data.Properties;
using Platformer.Model.Definitions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Platformer.Model.Definitions.Localization
{
	public class LocalizationManager
	{
		public readonly static LocalizationManager I;

		private StringPersistantProperty _localeKey = new StringPersistantProperty("en", "Localization/current");
		private Dictionary<string, string> _localization;


		public event Action OnLoclaeChanged;
		public string LocaleKey => _localeKey.Value;

		static LocalizationManager()
		{
			I = new LocalizationManager();
		}

		public LocalizationManager()
		{
			LoadLocale(_localeKey.Value);
		}

		

		internal string Localize(string key)
		{
			return _localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
		}

		public void LoadLocale(string localeToLoad)
		{
			var def = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
			
			_localization = def.GetData();
			_localeKey.Value = localeToLoad;
			OnLoclaeChanged?.Invoke();
		}

		internal void SetLocale(string localeKey)
		{
			LoadLocale(localeKey);
		}
	}


}