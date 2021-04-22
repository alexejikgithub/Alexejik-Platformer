using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Platformer.Components.Collectables
{

	public class CoinComponent : MonoBehaviour
	{
		[SerializeField] private int _coinAmount;
		
	


		public void AddCoins(GameObject target)
		{
			var coinCounter = target.GetComponent<CoinCounter>();
			coinCounter.AddCoinsToCounter(_coinAmount);
		}
	}
	
}
