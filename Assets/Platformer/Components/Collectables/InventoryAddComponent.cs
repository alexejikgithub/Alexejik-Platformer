using Assets.Platformer.Model.Data;
using Platformer.Creatures.Hero;
using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Repositories.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Components.Collectables
{

	public class InventoryAddComponent : MonoBehaviour
	{
		[InventoryId] [SerializeField] private string _id;
		[SerializeField] private int _count;
		[SerializeField] private UnityEvent _action;



		[SerializeField] private InventoryCollectableParent _inventoryCollectableParent;
		

		void Start()
		{
			_inventoryCollectableParent = GetComponentInParent<InventoryCollectableParent>();
			
				
		}
		public void Add(GameObject go)
		{

			
			if(_inventoryCollectableParent!=null)
			{
				var itemDef = DefsFacade.I.Items.Get(_id);
				var session = _inventoryCollectableParent.Session;
				var isFull = session.Data.Inventory.Inventory.Count >= DefsFacade.I.Player.InventorySize;
				var item = session.Data.Inventory.GetItem(_id);
				

				if (session != null)
				{

					if (itemDef.HasTag(ItemTag.Stackable) && session.Data.Inventory.Count(_id) >= itemDef.MaxAmount) return;
					if (item == null && isFull) return;
					if (!itemDef.HasTag(ItemTag.Stackable) && session.Data.Inventory.Count(_id) >= itemDef.MaxAmount && isFull) return;
					
				}
			}
			// If invantory is full, do not collect

			var hero = go.GetComponent<ICanAddInInventory>();
			hero?.AddInInventory(_id, _count);
			
			_action?.Invoke();
		}
		

	}
}