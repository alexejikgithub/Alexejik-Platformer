using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Player;
using Platformer.UI.Widgets;
using Platformer.UI.Windows.Perks;
using Platformer.Utils;
using Platformer.Utils.Disposables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
		[SerializeField] private PerksDisplayWidget _currentPerk;

		private GameSession _session;
		private readonly CompositeDisposable _trash = new CompositeDisposable();

		private void Awake()
		{
			
			
		}

		private void Start()
		{

			_session = GameSession.Instance;
			_trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHealthChanged));
			//_session.Data.Hp.OnChanged -= OnHealthChanged; // костыль для перехода в сцены
			//_session.Data.Hp.OnChanged += OnHealthChanged;
			_trash.Retain(_session.PerksModel.Subscribe(OnPerkChanged));
			// OnHealthChanged(_session.Data.Hp.Value, _session.Data.Hp.Value);
			OnPerkChanged();

		}

		
		private void OnPerkChanged()
		{
			var usedPerkId = _session.PerksModel.Used;
			var hasPerk = !string.IsNullOrEmpty(usedPerkId);
			if (hasPerk)
			{
				var perkDef = DefsFacade.I.Perks.Get(usedPerkId);
				_currentPerk.Set(perkDef);
			}
			//_currentPerk.gameObject.SetActive(hasPerk);
		}


		private void OnHealthChanged(int newValue, int oldValue)
		{

			var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
			var value = (float)newValue / maxHealth;
			_healthBar.SetProgress(value);
		}

		private void OnDestroy()
		{

			_trash.Dispose();
			//if (_session!=null)
			//{
			//	_session.Data.Hp.OnChanged -= OnHealthChanged;
			//}
			
		}

		public void ShowDebug()
		{
			WindowUtils.CreateWindow("UI/PlayerStatsWindow");
		}
	}
}