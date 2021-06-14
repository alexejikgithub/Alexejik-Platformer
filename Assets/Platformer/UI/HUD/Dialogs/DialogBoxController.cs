using Platformer.Model.Definitions;
using Platformer.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.HUD.Dialogs
{
	public class DialogBoxController : MonoBehaviour
	{

		
		[SerializeField] private GameObject _container;
		[SerializeField] private Animator _animator;
		

		[Space]

		[SerializeField] private float _textSpeed = 0.09f;

		[Header("Sounds")]
		[SerializeField] private AudioClip _typing;
		[SerializeField] private AudioClip _open;
		[SerializeField] private AudioClip _close;
		[Space] [SerializeField] protected DialogContent _content;

		private static readonly int IsOpen = Animator.StringToHash("IsOpen");

		private DialogData _data;
		private int _currentSentence;
		private AudioSource _sfxSource;
		private Coroutine _typingRoutine;

		protected DalogSentenceData CurrentSentence => _data.Sentences[_currentSentence];

		private void Start()
		{
			_sfxSource = AudioUtils.FindSfxSourse();
			//_icon.gameObject.SetActive(false);

		}

		public void ShowDialog(DialogData data)
		{
			_data = data;
			_currentSentence = 0;
			CurrentContent.Text.text = string.Empty;
			_container.SetActive(true);

			if (_sfxSource==null)
			{
				_sfxSource = AudioUtils.FindSfxSourse();
			}
			_sfxSource.PlayOneShot(_open);
			_animator.SetBool(IsOpen, true);
		}
		//private void SetIcon(DialogData data)
		//{

		//	if (data.GetIconSprite(_currentSentence) != null)
		//	{
		//		var currentIconSprite = data.GetIconSprite(_currentSentence);
		//		_icon.gameObject.SetActive(true);
		//		_icon.sprite = currentIconSprite;
		//	}
		//	else
		//	{
		//		_icon.gameObject.SetActive(false);
		//	}
		//}
		//private void SetSide(DialogData data)
		//{
		//	Side side = data.Sentences[_currentSentence].DialogBoxSide;
		//	var dialogBoxRectTransform = gameObject.GetComponent<RectTransform>();

		//	switch (side)
		//	{
		//		case Side.Center:
		//			dialogBoxRectTransform.anchoredPosition = new Vector2(0, dialogBoxRectTransform.anchoredPosition.y);
		//			return;
		//		case Side.Left:
		//			dialogBoxRectTransform.anchoredPosition = new Vector2(-50, dialogBoxRectTransform.anchoredPosition.y);
		//			return;
		//		case Side.Right:
		//			dialogBoxRectTransform.anchoredPosition = new Vector2(50, dialogBoxRectTransform.anchoredPosition.y);
		//			return;
		//	}
		//}


		

		private IEnumerator TypeDialogText()
		{
			CurrentContent.Text.text = string.Empty;
			var sentances = CurrentSentence;
			CurrentContent.TrySetIcon(CurrentSentence.IconSprite);
			foreach (var letter in sentances.Value)
			{
				CurrentContent.Text.text += letter;
				_sfxSource.PlayOneShot(_typing);
				yield return new WaitForSeconds(_textSpeed);
			}
			_typingRoutine = null;
		}

		protected virtual DialogContent CurrentContent => _content;

		public void OnSkip()
		{
			if (_typingRoutine == null) return;
			StopTypeAnimation();
			CurrentContent.Text.text = _data.Sentences[_currentSentence].Value;
		}
		public void OnContinue()
		{
			StopTypeAnimation();
			_currentSentence++;
			var isDialogComplete = _currentSentence >= _data.Sentences.Length;
			if (isDialogComplete)
			{
				HideDialogBox();
			}
			else
			{
				OnStartDialogAnimation();
				// SetIcon(_data);
				// SetSide(_data);
			}
		}

		private void HideDialogBox()
		{
			_animator.SetBool(IsOpen, false);
			_sfxSource.PlayOneShot(_close);

		}

		private void StopTypeAnimation()
		{
			if (_typingRoutine != null)
			{
				StopCoroutine(_typingRoutine);
			}
			_typingRoutine = null;

		}

		protected virtual void OnStartDialogAnimation()
		{
			//SetIcon(_data);
			// SetSide(_data);
			_typingRoutine = StartCoroutine(TypeDialogText());
		}



		public void OnCloseAnimationComplete()
		{
			_container.SetActive(false);
		}


	}
}