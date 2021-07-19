using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Components.LevelManagement
{
	public class ReloadLevelComponent : MonoBehaviour
	{
		public void Reload()
		{
			var session = GameSession.Instance;
			session.LoadLastSave();

			var scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);

		}
	}

}