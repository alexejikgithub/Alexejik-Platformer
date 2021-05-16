using Platformer.Model.Data;
using Platformer.Model.Data.Properties;
using System;
using System.Collections;
using UnityEngine;
using static Platformer.Model.Data.GameSettings;

namespace Platformer.Components.Audio
{

	[RequireComponent(typeof(AudioSource))]
	public class AudioSettingsComponent : MonoBehaviour
	{
		[SerializeField] private SoundSetting _mode;
		private FloatPersistentProperty _model;
		private AudioSource _sourse;

		private void Start()
		{
			_sourse = GetComponent<AudioSource>();
			_model = FindProperty();
			_model.OnChanged += OnSoundSettingsChanged;
			OnSoundSettingsChanged(_model.Value, _model.Value);

		}

		

		private void OnSoundSettingsChanged(float newValue, float oldValue)
		{
			_sourse.volume = newValue;
		}

		private FloatPersistentProperty FindProperty()
		{
			switch (_mode)
			{
				case SoundSetting.Music:
					return GameSettings.I.Music;
				case SoundSetting.Sfx:
					return GameSettings.I.Sfx;
			}

			throw new ArgumentException("Undefined mode");


		}
		private void OnDestroy()
		{
			_model.OnChanged -= OnSoundSettingsChanged;
		}

	}
}