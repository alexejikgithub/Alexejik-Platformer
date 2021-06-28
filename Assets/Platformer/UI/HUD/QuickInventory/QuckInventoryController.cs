using Platformer.Model;
using Platformer.Model.Data;
using Platformer.UI.Widgets;
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

		private DataGroup<InventoryItemData, InventoryItemWidget> _dataGroup;

		


		private void Start()
		{
			_dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget>(_prefab, _container);
			_session = FindObjectOfType<GameSession>();
			_trash.Retain(_session.QuickInventory.Subscribe(Rebuild));
			Rebuild();
		}

		private void Rebuild()
		{
			var inventory = _session.QuickInventory.Inventory;
			_dataGroup.SetData(inventory);
		}


		private void OnDestroy()
		{
			_trash.Dispose();
		}
	}
}