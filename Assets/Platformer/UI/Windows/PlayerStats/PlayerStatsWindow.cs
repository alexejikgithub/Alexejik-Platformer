using Assets.Platformer.UI.Windows.PlayerStats;
using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Player;
using Platformer.UI.Widgets;
using Platformer.Utils;
using Platformer.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Windows.PlayerStats
{
	public class PlayerStatsWindow : AnimatedWindow
	{
		[SerializeField] private Transform _statsContainer;
		[SerializeField] private StatWidget _prefab;

		[SerializeField] private Button _upgradeButton;
		[SerializeField] private ItemWidget _price;

		private DataGroup<StatDef, StatWidget> _dataGroup;

		private GameSession _session;
		private readonly CompositeDisposable _trash = new CompositeDisposable();

		protected override void Start()
		{
			base.Start();

			_dataGroup = new DataGroup<StatDef, StatWidget>(_prefab, _statsContainer);

			_session = FindObjectOfType<GameSession>();
			_session.StatsModel.InterfaceSelectedStat.Value = DefsFacade.I.Player.Stats[0].Id;
			_trash.Retain(_session.StatsModel.Subscribe(OnStatsChanged));
			_trash.Retain(_upgradeButton.onClick.Subscribe(OnUpgrade));

			OnStatsChanged();

		}

		public void OnUpgrade()
		{
			
			var selected = _session.StatsModel.InterfaceSelectedStat.Value;
			_session.StatsModel.LevelUp(selected);
		}

		private void OnStatsChanged()
		{
			var stats = DefsFacade.I.Player.Stats;
			_dataGroup.SetData(stats);
			var selected = _session.StatsModel.InterfaceSelectedStat.Value;
			var nextLevel = _session.StatsModel.GetCurrentLevel(selected) + 1;
			var def = _session.StatsModel.GetLevelDef(selected,nextLevel);
			_price.SetData(def.Price);
			_price.gameObject.SetActive(def.Price.Count != 0);
			_upgradeButton.gameObject.SetActive(def.Price.Count != 0);
		}

		private void OnDestroy()
		{
			_trash.Dispose();
		}
	}
}
