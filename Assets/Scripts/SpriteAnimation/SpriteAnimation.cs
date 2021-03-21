using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer
{
	[RequireComponent(typeof(SpriteRenderer))]

	public class SpriteAnimation : MonoBehaviour
	{
		[SerializeField] private string _nameOfTheClip; //Set name of the clip that you want to play
		[SerializeField] private int _frameRate;
		private Sprite[] _sprites; // no need to make [SerializeField] because _sprites come from _animationStates._Sprites
		[SerializeField] private UnityEvent _onComplete;
		[SerializeField] private MyAnimationStates[] _animationStates; // Need to choose objects in the inspector
		private MyAnimationStates _currentAnimationState; // 

		private SpriteRenderer _renderer;
		private float _secondsPerFrame;
		private int _currentSpriteIndex;
		private float _nextFrameTime;



		private void Start()
		{
			_renderer = GetComponent<SpriteRenderer>();
			



		}
		private void OnEnable()
		{
			_secondsPerFrame = 1f / _frameRate;
			_nextFrameTime = Time.time + _secondsPerFrame;
			_currentSpriteIndex = 0;
			_currentAnimationState = _animationStates[0];

		}

		private void Update()
		{
			if(_nameOfTheClip!=null)
			{
				SetClip(_nameOfTheClip); // change _currentAnimationState
			}
			


			_sprites = _currentAnimationState._sprites; // if _currentAnimationState is changed, sprites also change
			if (_nextFrameTime > Time.time)
			{
				return;
			}


			if (_currentSpriteIndex >= _sprites.Length)
			{
				if (_currentAnimationState._loop)
				{
					_currentSpriteIndex = 0;
				}

				else
				{


					_onComplete?.Invoke();
					enabled = false;
					return;

				}
			}
			_renderer.sprite = _sprites[_currentSpriteIndex];
			_nextFrameTime += _secondsPerFrame;
			_currentSpriteIndex++;


			

		}
		public void SetClip(string name)
		{
			for (int i = 0; i < _animationStates.Length; i++)
			{
				if (_animationStates[i]._name == name)
				{
					_currentAnimationState = _animationStates[i];
				}
			}
		}



	}
}

