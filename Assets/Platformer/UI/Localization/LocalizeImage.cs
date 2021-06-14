using Assets.Platformer.Model.Definitions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Localization
{
	public class LocalizeImage : AbstractLocalizeComponent
	{
		[SerializeField] private IconId[] _icons;
		[SerializeField] private Image _icon;
		protected override void Localize()
		{
			var iconData = _icons.FirstOrDefault(x => x.Id == LocalizationManager.I.LocaleKey);
			if(iconData!=null)
			{
				_icon.sprite = iconData.Icon;
			}
		}
	}

	[Serializable]
	public class IconId
	{
		public string Id;
		public Sprite Icon;
	}
}
