using Platformer.Model.Definitions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Model.Definitions.Player
{
	[Serializable]
	public class StatDef
	{
		[SerializeField] private string _name;
		[SerializeField] private StatId _id;
		[SerializeField] private Sprite _icon;
		[SerializeField] private StatLevelDef[] _levels;

		public StatId Id => _id;
		public string Name => _name;
		public Sprite Icon => _icon;
		public StatLevelDef[] Levels => _levels;

	}


	public enum StatId
	{
		Hp,
		Speed,
		RangeDamage
	}

	[Serializable]
	public struct StatLevelDef
	{
		[SerializeField] private float _value;
		[SerializeField] private ItemWithCount _price;

		public float Value => _value;
		public ItemWithCount Price => _price;
	}

}
