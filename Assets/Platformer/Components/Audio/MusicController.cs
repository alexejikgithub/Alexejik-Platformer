using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Platformer.Components.Audio

{
	public class MusicController : MonoBehaviour
	{
		[SerializeField] private AudioMixer _mixer;
		[ContextMenu("On")]
		public void SetEchoOn()
		{
			_mixer.SetFloat("Echo", 0);
		}

		[ContextMenu("Off")]
		public void SetEchoOff()
		{
			_mixer.SetFloat("Echo", -80);
		}
	}
}
