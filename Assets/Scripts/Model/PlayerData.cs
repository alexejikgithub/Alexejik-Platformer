using System;

namespace Platformer.Model
{
	[Serializable]
	public class PlayerData 
	{
		public int coins;
		public int Hp;
		public int SwordsCount = 0;
		public bool IsArmed=> SwordsCount>=1;

	}
}


