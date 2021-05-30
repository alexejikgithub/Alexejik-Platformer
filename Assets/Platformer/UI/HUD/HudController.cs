using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.UI.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

		private GameSession _sesson;
		private void Awake()
		{
			
			
		}

		private void Start()
		{

			OnLoad();

		}

		public void OnLoad()
		{
			_sesson = FindObjectOfType<GameSession>();
			_sesson.Data.Hp.OnChanged += OnHealthChanged;
			OnHealthChanged(_sesson.Data.Hp.Value, _sesson.Data.Hp.Value);
		}


		private void OnHealthChanged(int newValue, int oldValue)
		{
			Debug.Log("QQQQQQQQQQ");
			var maxHealth = DefsFacade.I.Player.MaxHealth;
			var value = (float)newValue / maxHealth;
			_healthBar.SetProgress(value);
		}

		private void OnDestroy()
		{
			
			if (_sesson!=null)
			{
				_sesson.Data.Hp.OnChanged -= OnHealthChanged;
			}
			
		}
	}
}