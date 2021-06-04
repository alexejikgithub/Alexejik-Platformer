

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Model.Data.Properties
{
	[Serializable]
	public class PerksData
	{
		[SerializeField] private StringProperty _used = new StringProperty();
		[SerializeField] private List<string> _unlocked;

		public StringProperty Used => _used;

		public void AddPerk(string id)
		{
			if (!IsUnlocked(id))
			{
				_unlocked.Add(id);
			}

		}
		public bool IsUnlocked(string id)
		{
			return _unlocked.Contains(id);
		}
	}
}
