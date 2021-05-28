using Assets.Platformer.Model.Definitions.Localization;
using System;
using System.Collections;
using UnityEngine;

namespace Platformer.Model.Definitions
{
	[Serializable]
	public class DalogSentenceData
	{
		//[SerializeField] private string _sentence;
		[SerializeField] private Sprite _iconSprite;
		[SerializeField] private Side _dialogBoxSide;
		[SerializeField] private string _key;

		public string Sentence => LocalizationManager.I.Localize(_key);
		public Sprite IconSprite => _iconSprite;
		public Side DialogBoxSide => _dialogBoxSide;

		public enum Side
		{
			Center,
			Left,
			Right

		}
	}
}