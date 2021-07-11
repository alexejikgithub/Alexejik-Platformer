using Platformer.Components.Health;
using Platformer.Utils;
using Platformer.Utils.Disposables;
using System;
using System.Collections;
using UnityEngine;

namespace Platformer.UI.Widgets
{
	public class BossHpWidget : MonoBehaviour
	{

		[SerializeField] private HealthComponent _health;
		[SerializeField] private ProgressBarWidget _hpBar;
		[SerializeField] private CanvasGroup _canvas;

		private readonly CompositeDisposable _trash = new CompositeDisposable();

		private float _maxHealth;

		private void Start()
		{
			_maxHealth = _health.Health;
			_trash.Retain(_health._onChange.Subscribe(OnHealthChanged));
			_trash.Retain(_health._onDie.Subscribe(HideUi));
		}

		[ContextMenu("Show")]
		public void ShowUi()
		{
			this.LerpAnimated(0, 1, 1, SetAlpha);
		}

		private void SetAlpha(float alpha)
		{
			_canvas.alpha = alpha;
		}

		[ContextMenu("Hide")]
		private void HideUi()
		{
			this.LerpAnimated(1, 0, 1, SetAlpha);
		}

		private void OnHealthChanged(int hp)
		{
			_hpBar.SetProgress(hp / _maxHealth);
		}

		private void OnDestroy()
		{
			_trash.Dispose();
		}
	}
}