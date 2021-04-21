using Platformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Components.LevelManagement
{
	public class ExitLevelComponent : MonoBehaviour
	{
		[SerializeField] private string _sceneName;
		public void Exit()
		{
			var session = FindObjectOfType<GameSession>();
			session.Save();
			SceneManager.LoadScene(_sceneName);
		}
	}
}

