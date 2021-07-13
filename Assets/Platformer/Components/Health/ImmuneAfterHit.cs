using Platformer.Utils;
using Platformer.Utils.Disposables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Components.Health
{
	[RequireComponent(typeof(HealthComponent))]
	public class ImmuneAfterHit : MonoBehaviour
	{
		[SerializeField] private float _immuneTime;
		private HealthComponent _health;
		private readonly CompositeDisposable _trash = new CompositeDisposable();
		private Coroutine _coroutine;

		private void Awake()
		{
			_health = GetComponent<HealthComponent>();
			_trash.Retain(_health._onDamage.Subscribe(OnDamage));
		}

		private void OnDamage()
		{
			TryStop();
			if (_immuneTime>0)
			{
				_coroutine = StartCoroutine(MakeImmune());
			}
		}
		private void TryStop()
		{
			if(_coroutine != null)
			{
				StopCoroutine(_coroutine);
			}
			_coroutine = null;
		}

		

		private IEnumerator MakeImmune()
		{
			_health.Immune.Retain(this);
			yield return new WaitForSeconds(_immuneTime);
			_health.Immune.Release(this);

		}

		private void OnDestroy()
		{
			TryStop();
			_trash.Dispose();
		}
	}
}
