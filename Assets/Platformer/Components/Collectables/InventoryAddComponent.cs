using Platformer.Creatures.Hero;
using Platformer.Model.Definitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components.Collectables
{

	public class InventoryAddComponent : MonoBehaviour
	{
		[InventoryId] [SerializeField] private string _id;
		[SerializeField] private int _count;

		public void Add(GameObject go)
		{
			var hero = go.GetComponent<Hero>();
			if (hero != null)
			{
				hero.AddInInventory(_id, _count);
			}
		}

	}
}