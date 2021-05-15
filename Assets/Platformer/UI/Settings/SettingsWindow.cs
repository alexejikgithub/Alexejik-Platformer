using Platformer.Model.Data;
using Platformer.UI.Widgets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.UI.Settings
{
    public class SettingsWindow : AnimatedWindow
    {

		[SerializeField] private AudioSettingsWidget _music;
		[SerializeField] private AudioSettingsWidget _sfx;



		protected override void Start()
		{
			base.Start();

			_music.SetModel(GameSettings.I.Music);
			_sfx.SetModel(GameSettings.I.Sfx);
		}
	}
}
