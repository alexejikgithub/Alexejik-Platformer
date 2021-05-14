using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI.MainMenu
{
	public class MainMenuWindow : AnimatedWindow

	{

		private Action _closeAction;

		public void OnStartGame()
		{
			_closeAction = () => { SceneManager.LoadScene("Level1"); };
			Close();
		}
		public void OnShowSettings()
		{
			var window = Resources.Load<GameObject>("UI/SettingsWindow");
			var canvas = FindObjectOfType<Canvas>();
			Instantiate(window, canvas.transform);
		}
		public void OnExit()
		{
			_closeAction = () =>
			{
				base.OnCloseAnimationComplete();

				Application.Quit();

#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#endif
			};
			Close();
		}

		public override void OnCloseAnimationComplete()
		{
			_closeAction?.Invoke();
			base.OnCloseAnimationComplete();

			
		}

	}
}