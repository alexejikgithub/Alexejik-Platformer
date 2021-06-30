using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Repositories;
using Platformer.UI.Widgets;
using Platformer.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Windows.Perks
{
	class PerksDisplayWidget : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private Image _lockImage;

		private GameSession _session;

		private void Start()
		{
			OnLoad();
		}

		internal void Set(PerkDef perkDef)
		{
			
			_icon.sprite = perkDef.Icon; 
		}

		public void OnLoad()
		{
			_session = FindObjectOfType<GameSession>();
			_session.PerksModel.SelectPerk(_session.PerksModel.SelectedPerk);
		}


		private void Update()
		{
			
			var cooldown = _session.PerksModel.Cooldown;
			
			_lockImage.fillAmount = cooldown.RemainingTime / cooldown.Value;
		}


		//private GameSession _session;
		//private Cooldown _speedUpCooldown = new Cooldown();

		//public string UsedPerk => _session.Data.Perks.Used.Value;


		//public bool PerkIsReady 
		//{
		//	get { return _session.PerksModel.PerkIsReady; }
		//	set { _session.PerksModel.PerkIsReady = value ; }
		//}


		//private void Start()
		//{
		//	_session = FindObjectOfType<GameSession>();
		//	PerkIsReady = true;
		//	UpdateView();
		//	_speedUpCooldown.Value = _cooldownTime; 
		//}



		//[ContextMenu("UpdateView")]
		//public void UpdateView()
		//{

		//	var def = DefsFacade.I.Perks.Get(UsedPerk);
		//	if (def.Icon != null)
		//	{
		//		_icon.sprite = def.Icon;
		//		_icon.color = Color.white;
		//	}
		//}
		//public void PerkReload()
		//{
		//	PerkIsReady = false;
		//	_speedUpCooldown.Reset();
		//	StartCoroutine(UnLockAnimation());
		//}



		//private IEnumerator UnLockAnimation()
		//{
		//	_lockImage.fillAmount = 1;
		//	var waitTime = _speedUpCooldown.TimesLasts;
		//	while(!_speedUpCooldown.IsReady)
		//	{
		//		_lockImage.fillAmount = _speedUpCooldown.TimesLasts / waitTime;
		//		yield return new WaitForSeconds(0.1f);
		//	}
		//	_lockImage.fillAmount = 0;
		//	PerkIsReady = true;
		//}

	}
}
