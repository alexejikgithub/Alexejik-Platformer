using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Components
{
	public class ReloadLevelComponent : MonoBehaviour
	{
		public void Reload()
		{
			var session = FindObjectOfType<GameSession>();
			Destroy(session);

			var scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);

		}
	}

}