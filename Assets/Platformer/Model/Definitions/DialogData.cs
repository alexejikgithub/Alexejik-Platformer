using System.Collections;
using UnityEngine;
using System;
using Assets.Platformer.Model.Definitions.Localization;

namespace Platformer.Model.Definitions
{
	[Serializable]
	public class DialogData
	{
		[SerializeField] private DalogSentenceData[] _sentences;
		[SerializeField] private DialogType _type;

		public DalogSentenceData[] Sentences => _sentences;
		public DialogType Type => _type;

		public Sprite GetIconSprite(int index)
		{
			if (Sentences[index].IconSprite != null)
			{
				return Sentences[index].IconSprite;
			}
			else
			{
				return null;
			}
		}

	}
	[Serializable]
	public class DalogSentenceData
	{
		//[SerializeField] private string _sentence;
		[SerializeField] private Sprite _iconSprite;
		[SerializeField] private Side _dialogBoxSide;
		[SerializeField] private string _key;

		public string Value => LocalizationManager.I.Localize(_key);
		public Sprite IconSprite => _iconSprite;
		public Side DialogBoxSide => _dialogBoxSide;

		
	}
	public enum Side
	{
		//Center,
		Left,
		Right

	}

	public enum DialogType
	{
		Simple,
		Personalized
	}

}