using Platformer.Model;
using Platformer.Model.Definitions.Player;
using Platformer.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Effects.CameraRelated
{
	[RequireComponent(typeof(Animator))]
	public class BloodSplashOverlay : MonoBehaviour
	{
		[SerializeField] private Transform _overlay;

		private static readonly int Health = Animator.StringToHash("Health");

		private Animator _animator;
		private Vector3 _overScale;
		private GameSession _session;

		private CompositeDisposable _trash = new CompositeDisposable();


		private void Start()
		{
			_animator = GetComponent<Animator>();
			_overScale = _overlay.localScale - Vector3.one;

			_session = GameSession.Instance;
			_trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHpChanged));

		}

		private void OnHpChanged(int newValue, int _)
		{
			var maxHp = _session.StatsModel.GetValue(StatId.Hp);
			var hpNormalized = newValue / maxHp;
			_animator.SetFloat(Health, hpNormalized);

			var overlayModifier = Mathf.Max(hpNormalized - 0.3f, 0f);
			_overlay.localScale = Vector3.one + _overScale * overlayModifier;
		}

		private void OnDestroy()
		{
			_trash.Dispose();
		}
	}
}
