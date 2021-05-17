using Platformer.Utils;
using System.Collections;
using UnityEngine;

namespace Platformer.Components.Audio
{
	public class PlaySfxSound : MonoBehaviour
	{

		[SerializeField] private AudioClip _clip;
		private AudioSource _sourse;

		public void Play()
		{
			if (_sourse==null)
			{
				_sourse = AudioUtils.FindSfxSourse();
			}
			_sourse.PlayOneShot(_clip);
		}
	}
}