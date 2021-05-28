using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.UI.Widgets
{
	public class DataGroup <TDataType, TItemType> where TItemType:MonoBehaviour, IItemRenderer<TDataType>
	{
		private readonly List<TItemType> _createdItems = new List<TItemType>();

		private readonly TItemType _prefab;
		private readonly Transform _container;

		public DataGroup(TItemType prefab, Transform container)
		{
			_prefab = prefab;
			_container = container;
		}

		public void SetData(IList<TDataType> data)
		{
			// Create required items.
			for (var i = _createdItems.Count; i < data.Count; i++)
			{
				var item = Object.Instantiate(_prefab, _container);
				_createdItems.Add(item);
			}

			// Update data and activate.
			for (var i = 0; i < data.Count; i++)
			{
				_createdItems[i].SetData(data[i], i);
				_createdItems[i].gameObject.SetActive(true);

			}

			// Hide unused items.
			for (var i = data.Count; i < _createdItems.Count; i++)
			{
				_createdItems[i].gameObject.SetActive(false);
			}
		}

		
	}
	public interface IItemRenderer<TDataType>
	{
		void SetData(TDataType data, int index);

	}
}