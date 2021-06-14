using Platformer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Components
{
	public class RestoreStateComponent : MonoBehaviour
	{
		[SerializeField] private string _id;
		private GameSession _session;

		public string Id => _id;

		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
			var isDestroyed = _session.RestoreState(_id);
			if (isDestroyed)
			{
				Destroy(gameObject);
			}
		}


	}
}
