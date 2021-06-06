using Platformer.Model;
using Platformer.Model.Data;
using Platformer.Utils.Disposables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.UI.HUD.QuickInventory
{


	public class QuckInventoryController : MonoBehaviour
	{
		[SerializeField] private Transform _container;
		[SerializeField] private InventoryItemWidget _prefab;

		private readonly CompositeDisposable _trash = new CompositeDisposable();

		private GameSession _session;
		private List<InventoryItemWidget> _createdItems = new List<InventoryItemWidget>();

		private void Awake()
		{

			if (DoesThisExist())
			{
				Destroy(gameObject);
			}


		}
		private bool DoesThisExist()
		{
			var controllers = FindObjectsOfType<QuckInventoryController>();
			foreach (var controller in controllers)
			{
				if (controller != this)
					return true;

			}
			return false;
		}


		private void Start()
		{
			OnLoad();
		}

		public void OnLoad()
		{
			_session = FindObjectOfType<GameSession>();

			_trash.Retain(_session.QuickInventory.Subscribe(Rebuild));


			Rebuild();
		}

		private void Rebuild()
		{
			var inventory = _session.QuickInventory.Inventory;

			// Create required items.
			for (var i = _createdItems.Count; i < inventory.Length; i++)
			{
				var item = Instantiate(_prefab, _container);
				_createdItems.Add(item);
			}

			// Update data and activate.
			for (var i = 0; i < inventory.Length; i++)
			{
				_createdItems[i].SetData(inventory[i], i);
				_createdItems[i].gameObject.SetActive(true);

			}

			// Hide unused items.
			for (var i = inventory.Length; i < _createdItems.Count; i++)
			{
				
				Destroy(_createdItems[i].gameObject);
				_createdItems.RemoveAt(i);
			}





		}
	}
}