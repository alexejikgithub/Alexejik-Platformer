using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.UI.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Platformer.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

		private GameSession _sesson;
		private void Start()
		{
			_sesson = FindObjectOfType<GameSession>();
			_sesson.Data.Hp.OnChanged += OnHealthChanged;
			OnHealthChanged(_sesson.Data.Hp.Value, _sesson.Data.Hp.Value);

		}

		private void OnHealthChanged(int newValue, int oldValue)
		{
			var maxHealth = DefsFacade.I.Player.MaxHealth;
			var value = (float)newValue / maxHealth;
			_healthBar.SetProgress(value);
		}

		private void OnDestroy()
		{
			_sesson.Data.Hp.OnChanged -= OnHealthChanged;
		}
	}
}