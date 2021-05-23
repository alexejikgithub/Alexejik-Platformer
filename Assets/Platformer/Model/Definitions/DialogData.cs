using System.Collections;
using UnityEngine;
using System;

namespace Platformer.Model.Definitions
{
	[Serializable]
	public class DialogData
	{
		[SerializeField] private DalogSentenceData[] _sentences;

		public DalogSentenceData[] Sentences => _sentences;

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



}