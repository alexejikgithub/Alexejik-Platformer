using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Creatures;
using Platformer.Model;

namespace Platformer.Components

{
	public class ArmHeroComponent : MonoBehaviour
	{
		private GameSession _session;
		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
			if (_session.Data.IsArmed)
			{
				gameObject.SetActive(false);
			}
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