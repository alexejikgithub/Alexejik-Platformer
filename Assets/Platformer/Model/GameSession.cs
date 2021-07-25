
using Assets.Platformer.Components.LevelManagement;
using Platformer.Components.LevelManagement;
using Platformer.Model.Data;
using Platformer.Model.Definitions.Player;
using Platformer.Model.Models;
using Platformer.UI.HUD;
using Platformer.UI.HUD.QuickInventory;
using Platformer.UI.Windows.Perks;
using Platformer.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Model
{
	public class GameSession : MonoBehaviour
	{
		[SerializeField] private PlayerData _data;
		[SerializeField] private string _defaultCheckpoint;

		public static GameSession Instance { get; private set; }

		public PlayerData Data => _data;


		private PlayerData _save;

		private readonly CompositeDisposable _trash = new CompositeDisposable();

		public QuickInventoryModel QuickInventory { get; private set; }
		public PerksModel PerksModel { get; private set; }
		public StatsModel StatsModel { get; private set; }

		private readonly List<string> _checkpoints = new List<string>();

		// Previous attempt to make permanently destroyed objects
		// private readonly List<string> _levelItemsDead = new List<string>(); // list that stores destroyed prefabs



		private void Awake()
		{
			var existsingSession = GetExistingSession();
			if (existsingSession != null)
			{
				existsingSession.StartSession(_defaultCheckpoint);
				Destroy(gameObject);
				

			}
			else
			{

				Save();
				InitModels();
				
				DontDestroyOnLoad(this);
				Instance = this;
				StartSession(_defaultCheckpoint);
			}

			
			

			
		}

		private void StartSession(string defaultCheckpoint)
		{
			SetChecked(defaultCheckpoint);
			LoadUIs();
			SpawnHero();

			// Previous attempt to make permanently destroyed objects
			// EnableitemsOnLevel();
		}

		private void SpawnHero()
		{
			var checkpoints = FindObjectsOfType<CheckPointComponent>();
			var lastCheckpoint = _checkpoints.Last();

			foreach (var checkPoint in checkpoints)
			{
				if (checkPoint.Id == lastCheckpoint)
				{
					checkPoint.SpawnHero();
					return;
				}
			}

			// This code will allow to return to the previous level
			if (_checkpoints.Count > 1)
			{
				_checkpoints.RemoveAt(_checkpoints.Count - 1);
				SpawnHero();
			}
		}


		// Previous attempt to make permanently destroyed objects
		//private void EnableitemsOnLevel()
		//{
		//	var itemsToEnable = FindObjectsOfType<EnableItemManager>();
		//	foreach (var item in itemsToEnable)
		//	{
		//		if (!_levelItemsDead.Contains(item.Id))
		//		{

		//			item.Enable();
		//		}
		//	}
		//}


		private void InitModels()
		{
			
			QuickInventory = new QuickInventoryModel(Data);
			_trash.Retain(QuickInventory);

			
				PerksModel = new PerksModel(_data);
				_trash.Retain(PerksModel);
			
			

			StatsModel = new StatsModel(_data);
			_trash.Retain(StatsModel);

			_data.Hp.Value = (int)StatsModel.GetValue(StatId.Hp);
		}

		private void LoadUIs()
		{
			SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
			LoadOnScreenControls();
		}

		[Conditional("USE_ON_SCREEN_CONTROLS")]
		private void LoadOnScreenControls()
		{
			SceneManager.LoadScene("Controls", LoadSceneMode.Additive);
		}

		private GameSession GetExistingSession()
		{
			var sessions = FindObjectsOfType<GameSession>();
			foreach (var gameSession in sessions)
			{
				if (gameSession != this)
					return gameSession;

			}
			return null;
		}

		public void Save()
		{
			_save = _data.Clone();
		}

		public void LoadLastSave()
		{
			_data = _save.Clone();

			_trash.Dispose();
			InitModels();

		}
		private void OnDestroy()
		{
			if(Instance == this)
			{
				Instance = null;
			}
			_trash.Dispose();
		}


		internal bool IsChecked(string id)
		{
			return _checkpoints.Contains(id);
		}
		internal void SetChecked(string id)
		{
			if (!_checkpoints.Contains(id))
			{
				Save();
				_checkpoints.Add(id);
			}
		}
		//public void AddToLevelItemsDead(string id)
		//{
		//	_levelItemsDead.Add(id);
		//}

		private List<string> _removedItems = new List<string>();
		internal void StoreState(string InstanceId)
		{
			if(!_removedItems.Contains(InstanceId))
			{
				_removedItems.Add(InstanceId);
			}
			
		}

		internal bool RestoreState(string InstanceId)
		{
			return _removedItems.Contains(InstanceId);
		}
	}


}

