using System.Collections;
using UnityEngine;

namespace Platformer.Model.Data.Properties
{
	public class StringPersistantProperty : PrefsPersistentProperty<string>
	{


		public StringPersistantProperty(string defaultValue,string key) : base(defaultValue,key)
		{
			Init();
		}

		protected override string Read(string defaultValue)
		{
			return PlayerPrefs.GetString(Key, defaultValue);
		}

		protected override void Write(string defaultValue)
		{
			PlayerPrefs.SetString(Key, defaultValue);

		}
	}
}