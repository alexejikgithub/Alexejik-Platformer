using Platformer.Components.Health;
using Platformer.Utils;
using Platformer.Utils.Disposables;
using System;
using System.Collections;
using UnityEngine;

namespace Platformer.Creatures.Mobs.Boss
{
	public class HealthAnimationGlue : MonoBehaviour
	{

		[SerializeField] private HealthComponent _hp;
		[SerializeField] private Animator _animator;

		private static readonly int Health = Animator.StringToHash("health");

		private readonly CompositeDisposable _trash = new CompositeDisposable();

		private void Awake()
		{
			_trash.Retain(_hp._onChange.Subscribe(OnHealthChanged));
			OnHealthChanged(_hp.Health);

		}

		private void OnHealthChanged(int health)
		{
			_animator.SetInteger(Health, health);
		}

		private void OnDestroy()
		{
			_trash.Dispose();
		}
	}
}