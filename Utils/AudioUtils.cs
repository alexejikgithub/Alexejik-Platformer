using System.Collections;
using UnityEngine;

namespace Platformer.Utils
{
	public class AudioUtils 
	{
		private const string SfxSourseTag = "SfxAudioSource";
		public static AudioSource FindSfxSourse()
		{
			
			return GameObject.FindWithTag(SfxSourseTag).GetComponent<AudioSource>();
		}
		
	}
}