using System;
using UnityEngine;

namespace Platformer.Model.Data
{
	[Serializable]
	public class PlayerData 
	{

		[SerializeField] private InventoryData _inventory;
		public InventoryData Inventory => _inventory;

		public int Hp;
		
		

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


