using System;
using UnityEngine;

namespace Platformer.Model
{
	[Serializable]
	public class PlayerData 
	{
		public int Coins;
		public int Hp;
		public int SwordsCount = 0;
		public bool IsArmed=> SwordsCount>=1;

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


