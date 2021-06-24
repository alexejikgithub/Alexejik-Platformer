using Platformer.Model.Data;
using Platformer.Model.Data.Properties;
using Platformer.Model.Definitions;
using Platformer.Utils.Disposables;
using System;
using System.Net.Mime;

namespace Platformer.Model.Models
{
	public class PerksModel : IDisposable
	{
		private readonly PlayerData _data;
		public readonly StringProperty InterfaceSelection = new StringProperty();



		private readonly CompositeDisposable _trash = new CompositeDisposable();
		public event Action OnChanged;

		

		public PerksModel(PlayerData data)
		{
			_data = data;
			InterfaceSelection.Value = DefsFacade.I.Perks.All[0].Id;

			_trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
			_trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
		}
		public IDisposable Subscribe(Action call)
		{
			OnChanged += call;
			return new ActionDisposable(() => OnChanged -= call);
		}

		public string Used => _data.Perks.Used.Value;
		public bool IsSuperThrowSupported => _data.Perks.Used.Value == "super-throw";
		public bool IsDoubleJumpSupported => _data.Perks.Used.Value == "double-jump";
		public bool IsShieldSupported => _data.Perks.Used.Value == "shield";
		public bool IsDashSupported => _data.Perks.Used.Value == "dash";

		private bool _perkIsReady;
		public bool PerkIsReady
		{
			get { return _perkIsReady; }
			set { _perkIsReady = value; }
		}

		public void Unlock(string id)
		{
			var def = DefsFacade.I.Perks.Get(id);
			var isEnoughResourses = _data.Inventory.IsEnough(def.Price);

			if (isEnoughResourses)
			{
				_data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
				_data.Perks.AddPerk(id);
				OnChanged?.Invoke();
			}

		}


		internal void UsePerk(string selected)
		{
			_data.Perks.Used.Value = selected;
		}


		internal bool IsUsed(string perkId)
		{
			return _data.Perks.Used.Value == perkId;
		}

		internal bool IsUnlocked(string perkId)
		{
			return _data.Perks.IsUnlocked(perkId);
		}



		internal bool CanBuy(string perkId)
		{
			var def = DefsFacade.I.Perks.Get(perkId);
			return _data.Inventory.IsEnough(def.Price);
		}


		public void Dispose()
		{
			_trash.Dispose();
		}

		
	}
}
