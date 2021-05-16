using System.Collections;
using UnityEngine;

namespace Platformer.Model.Data.Properties
{
	public class ObservableProperty<TPropertyType>
	{

		[SerializeField] private TPropertyType _value;

		public delegate void OnPropertychanged(TPropertyType newValue, TPropertyType oldValue);

		public event OnPropertychanged OnChanged;

		public TPropertyType Value
		{
			get => _value;
			set
			{
				var isSame = _value.Equals(value);
				if (isSame) return;
				var oldValue = _value;

				_value = value;
				OnChanged?.Invoke(value, oldValue);
			}
		}
		
	}
}