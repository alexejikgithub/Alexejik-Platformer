using System.Collections;
using UnityEngine;
using System;

namespace Platformer.Model.Definitions
{
	[Serializable]
	public class DialogData 
	{
		[SerializeField] private string[] _sentences;
		public string[] Sentences => _sentences;
		
	}
}