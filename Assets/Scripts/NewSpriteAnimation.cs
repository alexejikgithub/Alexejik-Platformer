using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer
{
	[RequireComponent(typeof(SpriteRenderer))]

	public class NewSpriteAnimation : MonoBehaviour
	{
		[SerializeField] private string _nameOfTheClip; //Set name of the clip that you want to play
		[SerializeField] private int _frameRate;
		private Sprite[] _sprites; // no need to make [SerializeField] because _sprites come from _animationStates._Sprites
		[SerializeField] private UnityEvent _onComplete;
		[SerializeField] private StatesOfAnimation[] _animationStates; // Need to choose objects in the inspector
		private StatesOfAnimation _currentAnimationState; // 
		private int _currentAnimationIndex;

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
				_nameOfTheClip = null;
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
				else if (_currentAnimationState._allowNext&& _currentAnimationIndex< _animationStates.Length-1) // If Allow next animation state and there are animation states left
				{
					_currentSpriteIndex = 0;
					_currentAnimationIndex++;
					_currentAnimationState = _animationStates[_currentAnimationIndex++];
					

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
					_currentAnimationIndex = i;
				}
			}
		}



	}
	[Serializable]
	public class StatesOfAnimation
	{
		[SerializeField] public string _name;
		[SerializeField] public bool _loop;
		[SerializeField] public bool _allowNext;
		[SerializeField] public Sprite[] _sprites;
	}
}

