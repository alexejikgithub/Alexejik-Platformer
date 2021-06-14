using Assets.Platformer.Model.Definitions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.UI.Localization
{
	public abstract class AbstractLocalizeComponent : MonoBehaviour
	{
		protected virtual void Awake()
		{
			LocalizationManager.I.OnLoclaeChanged += OnLocaleChanged;
			Localize();
		}

		private void OnLocaleChanged()
		{
			Localize();
		}

		protected abstract void Localize();
		
		private void OnDestroy()
		{
			LocalizationManager.I.OnLoclaeChanged -= OnLocaleChanged;
		}
	}
}
