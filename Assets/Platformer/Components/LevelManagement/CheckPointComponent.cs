using Platformer.Components.GoBased;
using Platformer.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Components.LevelManagement
{
	[RequireComponent(typeof(SpawnComponent))]
	public class CheckPointComponent : MonoBehaviour
	{
		[SerializeField] private string _id;
		[SerializeField] private UnityEvent _setChecked;
		[SerializeField] private UnityEvent _setUnchecked;
		[SerializeField] private SpawnComponent _heroSpawner;
		public string Id => _id;

		private GameSession _session;
		

		
		private void Start()
		{
			
			_session = FindObjectOfType<GameSession>();
			if (_session.IsChecked(_id))
			{
				_setChecked?.Invoke();
			}
			else
			{
				_setUnchecked?.Invoke();
			}

		}
		public void Check()
		{
			_session.SetChecked(_id);
			_setChecked?.Invoke();
		}

		public void SpawnHero()
		{
			_heroSpawner.Spawn();
		}

	}
}