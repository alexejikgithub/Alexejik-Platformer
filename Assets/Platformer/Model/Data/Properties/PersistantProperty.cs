using Platformer.Utils.Disposables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Model.Data.Properties
{
   
    public abstract class PersistantProperty<TPropertyType>:ObservableProperty<TPropertyType>
    {

        
        protected TPropertyType _stored;

        private TPropertyType _defaultValue;


        public PersistantProperty(TPropertyType defaultValue)
		{
            _defaultValue = defaultValue;
		}

        public override TPropertyType Value
		{
            get => _stored;
            set
			{
                var isEqual = _stored.Equals(value);
                if (isEqual) return;

                var oldValue=_value;
                Write(value);
                _stored=_value = value;

                InvokeChangedEvent(value, oldValue);
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