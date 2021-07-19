using Platformer.Model.Definitions.Player;
using Platformer.UI.Widgets;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using Platformer.Model;
using Assets.Platformer.Model.Definitions.Localization;
using Platformer.Model.Definitions;
using System.Globalization;

namespace Assets.Platformer.UI.Windows.PlayerStats
{
	public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>

	{
		[SerializeField] private Text _name;
		[SerializeField] private Image _icon;
		[SerializeField] private Text _currentValue;
		[SerializeField] private Text _increaseValue;
		[SerializeField] private ProgressBarWidget _progress;
		[SerializeField] private GameObject _selector;

		private GameSession _session;
		private StatDef _data;

		private void Start()
		{
			_session = GameSession.Instance;
			UpdateView();
		}

		

		public void SetData(StatDef data, int index)
		{
			_data = data;
			if (_session != null)
			{
				UpdateView();
			}
		}

		[ContextMenu ("update")]
		private void UpdateView()
		{
			var statsModel = _session.StatsModel;
			_icon.sprite = _data.Icon;
			_name.text = LocalizationManager.I.Localize(_data.Name);
			var currentLevelValue = statsModel.GetValue(_data.Id);
			_currentValue.text = currentLevelValue.ToString(CultureInfo.InvariantCulture);

			var currentLevel = statsModel.GetCurrentLevel(_data.Id);
			var nextLevel = currentLevel + 1;
			var nextLevelValue = statsModel.GetValue(_data.Id, nextLevel);
			var increaseValue = nextLevelValue - currentLevelValue;
			_increaseValue.text = $"+ {increaseValue}";
			_increaseValue.gameObject.SetActive(increaseValue > 0);

			var maxLevelValue = DefsFacade.I.Player.GetStat(_data.Id).Levels.Length -1;			
			_progress.SetProgress(currentLevel/(float)maxLevelValue);
			_selector.SetActive(statsModel.InterfaceSelectedStat.Value == _data.Id);
		}
		
		public void OnSelect()
		{
			_session.StatsModel.InterfaceSelectedStat.Value = _data.Id;
		}
	}
}
