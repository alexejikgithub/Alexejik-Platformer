using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Player;
using Platformer.UI.Widgets;
using Platformer.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
		[SerializeField] private CoinCounterWidget _coinCounter;

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
			_sesson.Data.Hp.OnChanged -= OnHealthChanged; // костыль для перехода в сцены
			_sesson.Data.Hp.OnChanged += OnHealthChanged;
			OnHealthChanged(_sesson.Data.Hp.Value, _sesson.Data.Hp.Value);
			_coinCounter.OnLoad();
		}


		private void OnHealthChanged(int newValue, int oldValue)
		{

			var maxHealth = _sesson.StatsModel.GetValue(StatId.Hp);
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

		public void ShowDebug()
		{
			WindowUtils.CreateWindow("UI/PlayerStatsWindow");
		}
	}
}