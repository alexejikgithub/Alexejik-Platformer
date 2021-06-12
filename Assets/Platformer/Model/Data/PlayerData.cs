using Platformer.Model.Data.Properties;
using System;
using UnityEngine;

namespace Platformer.Model.Data
{
	[Serializable]
	public class PlayerData 
	{

		[SerializeField] private InventoryData _inventory;
		public InventoryData Inventory => _inventory;
		public PerksData Perks = new PerksData();
		public LevelData Levels = new LevelData();
		
		public IntProperty Hp =new IntProperty();
		
		

		public PlayerData Clone()
		{

			var json = JsonUtility.ToJson(this);
			return JsonUtility.FromJson<PlayerData>(json);
			//return new PlayerData
			//{
			//	Coins = Coins,
			//	Hp = Hp,
			//	SwordsCount = SwordsCount
			//};

		}
		

	}
}


