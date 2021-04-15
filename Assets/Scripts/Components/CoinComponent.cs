using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Platformer.Components
{

	public class CoinComponent : MonoBehaviour
	{
		[SerializeField] private int _coinAmount;
		private CoinCounter _coinCounter;

		private void Awake()
		{
			_coinCounter = (CoinCounter)FindObjectOfType(typeof(CoinCounter));
			
		}

		


		public void AddCoins()
		{
			_coinCounter.AddCoinsToCounter(_coinAmount);
		}
	}
	
}
