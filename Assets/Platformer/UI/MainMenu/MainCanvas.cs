using System.Collections;
using UnityEngine;

namespace Platformer.UI.MainMenu
{
	public class MainCanvas : MonoBehaviour
	{
		private void Awake()
		{
			if (IsCanvasExist())
			{
				Destroy(gameObject);

			}
			else
			{

				
				DontDestroyOnLoad(this);

			}
		}

		private bool IsCanvasExist()
		{
			var sessions = FindObjectsOfType<MainCanvas>();
			foreach (var gameSession in sessions)
			{
				if (gameSession != this)
					return true;

			}
			return false;
		}

	}
}