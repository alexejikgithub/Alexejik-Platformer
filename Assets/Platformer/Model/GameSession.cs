
using Platformer.Model.Data;
using Platformer.Utils.Disposables;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Model
{
	public class GameSession : MonoBehaviour
	{
		[SerializeField] private PlayerData _data;

		public PlayerData Data => _data;

		private PlayerData _save;

		private readonly CompositeDisposable _trash = new CompositeDisposable();

		public QuickInventoryModel QuickInventory { get; private set; }


		private void Awake()
		{
			
			LoadHud();
			
			if (IsSessionExist())
			{
				Destroy(gameObject);
				
			}
			else
			{
				
				Save();
				InitModels();
				DontDestroyOnLoad(this);
				
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

		private bool IsSessionExist()
		{
			var sessions = FindObjectsOfType<GameSession>();
			foreach (var gameSession in sessions)
			{
				if (gameSession != this)
					return true;

			}
			return false;
		}

		public void Save()
		{
			_save = _data.Clone();
		}

		public void LoadLastSave()
		{
			_data = _save.Clone();
		}
		private void OnDestroy()
		{
			_trash.Dispose();
		}
	}

	
}

