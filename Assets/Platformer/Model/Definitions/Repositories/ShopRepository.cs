using Platformer.Model.Definitions.Repositories.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Model.Definitions.Repositories
{

	// Is not required anymore. I will use existing inventoryItemsDef
	[CreateAssetMenu(menuName = "Defs/ShopRepo", fileName = "ShopRepo")]
	public class ShopRepository : DefRepository<ShopItemDef>
	{ 
	}

	[Serializable]
	public struct ShopItemDef : IHaveId
	{

		[InventoryId] [SerializeField] private string _id;
		[SerializeField] private int _price;
		[SerializeField] private int _defaultStock;
		public string Id => _id;
		public int Price => _price;
		public int DefaultStock => _defaultStock;

	}
}