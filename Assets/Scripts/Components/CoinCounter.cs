using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{


	public class CoinCounter : MonoBehaviour
	{
		private int _coinCount = 0;


		public void AddCoinsToCounter(int value)
		{
			_coinCount += value;
			Debug.Log(value + " coins added to the bag");
			Debug.Log("Now you have " + _coinCount + " coins");

		}
		public void RemoveCoinsFromCounter(int value)
		{
			_coinCount -= value;
			Debug.Log(value + " coins lost from the bag");
			Debug.Log("Now you have " + _coinCount + " coins");
		}
		public int GiveCoinAmount()
		{
			return _coinCount;
		}
	}

}