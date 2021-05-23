using System;
using System.Collections;
using UnityEngine;

namespace Platformer.Model.Definitions
{
	[Serializable]
	public class DalogSentenceData
	{
		[SerializeField] private string _sentence;
		[SerializeField] private Sprite _iconSprite;
		[SerializeField] private Side _dialogBoxSide;

		public string Sentence => _sentence;
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