using Platformer.Components.Audio;
using Platformer.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Platformer.UI.Widgets
{
	public class ButtonSound : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private AudioClip _audioClip;

		private AudioSource _source;
		public void OnPointerClick(PointerEventData eventData)
		{
			if (_source == null)
			{
				_source = AudioUtils.FindSfxSourse();
			}
			_source.PlayOneShot(_audioClip);
		}
	}
}