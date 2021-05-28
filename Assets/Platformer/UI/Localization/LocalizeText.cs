using Assets.Platformer.Model.Definitions.Localization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Platformer.UI.Localization
{
	[RequireComponent(typeof(Text))]
	public class LocalizeText : MonoBehaviour
	{
		[SerializeField] private string _key;
		[SerializeField] private bool _capitalize;

		private Text _text;

		private void Awake()
		{
			_text = GetComponent<Text>();
			LocalizationManager.I.OnLoclaeChanged += OnLocaleChanged;
			Localize();
		}

		private void OnLocaleChanged()
		{
			Localize();
		}

		private void Localize()
		{
			var localized = LocalizationManager.I.Localize(_key);
			_text.text = _capitalize ? localized.ToUpper() : localized;

		}
		private void OnDestroy()
		{
			LocalizationManager.I.OnLoclaeChanged -= OnLocaleChanged;
		}
	}
}