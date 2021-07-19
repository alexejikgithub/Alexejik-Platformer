using Platformer.Model;
using System.Collections;
using UnityEngine;

namespace Platformer.Components.Collectables
{
	// This sctipt will allow to find gamesession only once
	public class InventoryCollectableParent : MonoBehaviour
	{
		public GameSession Session;

		
		void Start()
		{
			Session = GameSession.Instance;
		}

	
	}
}