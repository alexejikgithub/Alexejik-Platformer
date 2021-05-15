using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Platformer.Components.Audio

{
	public class EchoController : MonoBehaviour
	{
		private AudioMixer _audioMixer;
		private AudioSource _source;
		public void SetEchoOn(GameObject target)
		{

			
					if (_source == null)
					{
						_source = GameObject.FindWithTag("MainMusicTheme").GetComponent<AudioSource>();
					}
					_audioMixer = _source.outputAudioMixerGroup.audioMixer;
			

			_audioMixer.SetFloat("Echo", 0);
		}

		[ContextMenu("Off")]
		public void SetEchoOff(GameObject target)
		{

			foreach (Transform child in target.transform)
			{
				if (child.CompareTag("MainMusicTheme"))
				{
					_audioMixer = child.GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
				}
			}

			_audioMixer.SetFloat("Echo", -80);
			
		}
	}
}
