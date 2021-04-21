using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Creatures;
using Platformer.Model;
using Platformer.Creatures.Hero;

namespace Platformer.Components.Collectables

{
	public class ArmHeroComponent : MonoBehaviour
	{
		private GameSession _session;
		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
			
		}
		public void ArmHero (GameObject go)
		{
			var hero = go.GetComponent<Hero>();
			if (hero!=null)
			{
				hero.ArmHero();
			}
		}
	}
}