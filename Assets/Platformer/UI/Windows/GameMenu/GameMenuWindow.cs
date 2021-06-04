using Platformer.Components.LevelManagement;
using Platformer.Model;
using Platformer.UI.MainMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Utils;

namespace Platformer.UI.GameMenu
{
	public class GameMenuWindow : AnimatedWindow
	{

		private Action _closeAction;
		private ReloadLevelComponent _reloader;
		private float _defaultTimescale;

		protected override void Start()
		{
			base.Start();
			_defaultTimescale = Time.timeScale;
			Time.timeScale = 0;
		}
		private void OnDestroy()
		{
			Time.timeScale = _defaultTimescale;
		}
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
			WindowUtils.CreateWindow("UI/SettingsWindow");
			
			
		}
		public void OnExit()
		{
			

				SceneManager.LoadScene("MainMenu");
				var session = FindObjectOfType<GameSession>();
				Destroy(session.gameObject);
				
			
		}

		public override void OnCloseAnimationComplete()
		{
			_closeAction?.Invoke();
			base.OnCloseAnimationComplete();


		}

	}
}
