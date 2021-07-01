using Platformer.Model;
using Platformer.UI.LevelsLoader;
using UnityEngine;


namespace Platformer.Components.LevelManagement
{
	public class ExitLevelComponent : MonoBehaviour
	{
		[SerializeField] private string _sceneName;
		public void Exit()
		{
			var session = FindObjectOfType<GameSession>();
			session.Save();

			var loader = FindObjectOfType<LevelLoader>();
			loader.LoadLevel(_sceneName);


		}
	}
}

