using Platformer.Components.LevelManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI.GameMenu
{
	public class GameMenuWindow : AnimatedWindow
	{

		private Action _closeAction;
		private ReloadLevelComponent _reloader;



		public void OnContinue()
		{

			Close();
		}
		public void OnRestart()
		{
			_reloader = FindObjectOfType<ReloadLevelComponent>();
			_closeAction = () =>
			{
				_reloader.Reload();
			};
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
