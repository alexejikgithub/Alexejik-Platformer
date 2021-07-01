using Platformer.UI.LevelsLoader;
using Platformer.Utils;
using System;

using UnityEngine;


namespace Platformer.UI.MainMenu
{
	public class MainMenuWindow : AnimatedWindow

	{

		private Action _closeAction;

		public void OnStartGame()
		{
			_closeAction = () =>
			{
				var loader = FindObjectOfType<LevelLoader>();
				loader.LoadLevel("Level2");

			};
			Close();
		}

		public void OnLanguages()
		{
			WindowUtils.CreateWindow("UI/LocalizationWindow");
		}
		public void OnShowSettings()
		{
			WindowUtils.CreateWindow("UI/SettingsWindow");
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