
using Platformer.Model.Definitions.Repositories;
using Platformer.Model.Definitions.Repositories.Items;
using System;
using UnityEngine;

namespace Platformer.Model.Definitions
{

	[CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
	public class DefsFacade : ScriptableObject
	{
		[SerializeField] private ItemsRepository _items;
		[SerializeField] private ThrowableItemsDef _throwableItems;
		[SerializeField] private PlayerDef _player;

		public ItemsRepository Items => _items;
		public PlayerDef Player => _player;


		private static DefsFacade _instance;
		public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

		public ThrowableItemsDef ThrowableItems => _throwableItems; 

		private static DefsFacade LoadDefs()
		{
			return _instance = Resources.Load<DefsFacade>("DefsFacade");
		}
	}
}
