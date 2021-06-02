
using Assets.Platformer.Components.LevelManagement;
using Platformer.Components.LevelManagement;
using Platformer.Model.Data;
using Platformer.UI.HUD;
using Platformer.UI.HUD.QuickInventory;
using Platformer.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Model
{
	public class GameSession : MonoBehaviour
	{
		[SerializeField] private PlayerData _data;
		[SerializeField] private string _defaultCheckpoint;

		public PlayerData Data => _data;

		private PlayerData _save;

		private readonly CompositeDisposable _trash = new CompositeDisposable();

		public QuickInventoryModel QuickInventory { get; private set; }

		private readonly List<string> _checkpoints = new List<string>();
		private readonly List<string> _levelItemsDead = new List<string>(); // list that stores destroyed prefabs



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
				StartSession(_defaultCheckpoint);
			}


			// reloads HudController
			var hud = FindObjectOfType<HudController>();
			if (hud != null)
			{
				hud.OnLoad();
			}


			// reloads QuckInventoryController
			var qInventory = FindObjectOfType<QuckInventoryController>();
			if (qInventory != null)
			{
				qInventory.OnLoad();
			}


		}

		private void StartSession(string defaultCheckpoint)
		{

			SetChecked(defaultCheckpoint);
			LoadHud();
			SpawnHero();
			EnableitemsOnLevel();
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

		private void EnableitemsOnLevel()
		{
			var itemsToEnable = FindObjectsOfType<EnableItemManager>();
			foreach (var item in itemsToEnable)
			{

				if (!_levelItemsDead.Contains(item.Id))
				{

					item.Enable();
				}

			}

		}


		private void InitModels()
		{

			QuickInventory = new QuickInventoryModel(Data);
			_trash.Retain(QuickInventory);
		}

		private void LoadHud()
		{
			SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
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
		public void AddToLevelItemsDead(string id)
		{
			_levelItemsDead.Add(id);
		}
	}


}

