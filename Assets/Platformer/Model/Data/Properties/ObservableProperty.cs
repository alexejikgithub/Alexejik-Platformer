using Platformer.Utils.Disposables;
using System;
using System.Collections;
using UnityEngine;

namespace Platformer.Model.Data.Properties
{
	[Serializable]
	public class ObservableProperty<TPropertyType>
	{

		[SerializeField] protected TPropertyType _value;

		public delegate void OnPropertychanged(TPropertyType newValue, TPropertyType oldValue);

		public event OnPropertychanged OnChanged;

		public IDisposable Subscribe(OnPropertychanged call)
		{
			OnChanged += call;
			return new ActionDisposable(() => OnChanged -= call);
		}
		
		public IDisposable SubscribeAndInvoke(OnPropertychanged call)
		{
			OnChanged += call;
			var dispose = new ActionDisposable(() => OnChanged -= call);
			call(_value, _value);
			return dispose;
		}
		public virtual TPropertyType Value
		{
			get => _value;
			set
			{
				var isSame = _value.Equals(value);
				if (isSame) return;
				var oldValue = _value;

				_value = value;
				InvokeChangedEvent(value, oldValue);
			}
		}

		protected void InvokeChangedEvent(TPropertyType newValue, TPropertyType oldValue)
		{
			OnChanged?.Invoke(newValue, oldValue);
		}

	}
}