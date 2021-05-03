using Assets.Platformer.Model.Data;
using Platformer.Creatures.Hero;
using Platformer.Model;
using Platformer.Model.Definitions;
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
			_inventoryCollectableParent = (InventoryCollectableParent)GetComponentInParent(typeof(InventoryCollectableParent));
			
				
		}
		public void Add(GameObject go)
		{
			
			if(_inventoryCollectableParent!=null)
			{
				var itemDef = DefsFacade.I.Items.Get(_id);
				var session = _inventoryCollectableParent.Session;
				
				if (session != null)
				{

					if (session.Data.Inventory.Count(_id) >= itemDef.MaxAmount && !itemDef.Stackable)
					{
						return;
					}
				}
			}
			// If invantory is full, do not collect

			var hero = go.GetComponent<ICanAddInInventory>();
			hero?.AddInInventory(_id, _count);
			
			_action?.Invoke();
		}
		

	}
}