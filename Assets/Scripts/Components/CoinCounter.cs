using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{


	public class CoinCounter : MonoBehaviour
	{
		private GameSession _session;

		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
		}
		public void AddCoinsToCounter(int value)
		{
			_session.Data.Coins += value;
			Debug.Log(value + " coins added to the bag");
			Debug.Log("Now you have " + _session.Data.Coins + " coins");

		}
		public void RemoveCoinsFromCounter(int value)
		{
			_session.Data.Coins -= value;
			Debug.Log(value + " coins lost from the bag");
			Debug.Log("Now you have " + _session.Data.Coins + " coins");
		}
		public int GiveCoinAmount()
		{
			return _session.Data.Coins;
		}
	}

}