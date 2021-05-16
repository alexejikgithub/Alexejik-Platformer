﻿using System.Collections;
using UnityEngine;

namespace Platformer.Model.Data.Properties
{
	public abstract class PrefsPersistentProperty<TPropertyType> : PersistantProperty<TPropertyType>
	{
		protected string Key;

		protected PrefsPersistentProperty(TPropertyType defaultValue, string key):base (defaultValue)
		{
			Key = key;
		}
		
		
	}
}