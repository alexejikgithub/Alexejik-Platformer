using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Model.Data.Properties
{
    [Serializable]
    public abstract class PersistentProperty<TPropertyType>
    {

        [SerializeField] private TPropertyType _value;
        private TPropertyType _stored;

        private TPropertyType _defaultValue;

        public delegate void OnPropertychanged(TPropertyType newValue, TPropertyType oldValue);

        public event OnPropertychanged onChanged;

        public PersistentProperty(TPropertyType defaultValue)
		{
            _defaultValue = defaultValue;
		}

        public TPropertyType Value
		{
            get => _stored;
            set
			{
                var isEqual = _stored.Equals(value);
                if (isEqual) return;

                var oldValue=_value;
                Write(value);
                _stored=_value = value;

                onChanged?.Invoke(value, oldValue);
			}
		}
        public void Init()
		{
            _stored =_value = Read(_defaultValue);
		}

        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType _defaultValue);

        public void Validate()
		{
            if(!_stored.Equals(_value))
			{
                Value = _value;
			}

		}
    }
}