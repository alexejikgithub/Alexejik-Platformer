using System;
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
			if (_nameOfTheClip != null)
			{
				SetClip(_nameOfTheClip); // change _currentAnimationState
				_nameOfTheClip = null;
			}



			_sprites = _currentAnimationState.Sprites; // if _currentAnimationState is changed, sprites also change
			if (_nextFrameTime > Time.time)
			{
				return;
			}


			if (_currentSpriteIndex >= _sprites.Length)
			{
				if (_currentAnimationState.Loop)
				{
					_currentSpriteIndex = 0;
				}
				else if (_currentAnimationState.AllowNext) // If Allow next animation state and there are animation states left
				{
					_currentSpriteIndex = 0;
					if (_currentAnimationIndex == _animationStates.Length - 1)
					{
						_currentAnimationIndex = 0;
					}
					else _currentAnimationIndex++;
					_currentAnimationState = _animationStates[_currentAnimationIndex];


				}

				else
				{


					_currentAnimationState.OnComplete?.Invoke();
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
				if (_animationStates[i].Name == name)
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
		[SerializeField] private string _name;
		[SerializeField] private bool _loop;
		[SerializeField] private bool _allowNext;
		[SerializeField] private Sprite[] _sprites;
		[SerializeField] private UnityEvent _onComplete;


		public string Name => _name;
		public bool Loop => _loop;
		public bool AllowNext => _allowNext;
		public Sprite[] Sprites => _sprites;
		public UnityEvent OnComplete => _onComplete;


	}
}

