
using UnityEngine;


namespace Platformer.Model
{
	public class GameSession : MonoBehaviour
	{
		[SerializeField] private PlayerData _data;

		public PlayerData Data => _data;

		private PlayerData _save;


		private void Awake()
		{
			if (IsSessionExist())
			{
				Destroy(gameObject);
			}
			else
			{
				Save();
				DontDestroyOnLoad(this);
			}
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
	}
}

