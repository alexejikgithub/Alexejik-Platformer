using Platformer.Components.GoBased;
using Platformer.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Platformer.Components.LevelManagement
{
	
	public class EnableItemManager : MonoBehaviour
	{
		[SerializeField] private string _id;
		[SerializeField] private GameObject _item;
		public string Id => _id;


		private GameSession _session;




		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
		}
		public void ChangeToDead()
		{
			_session.AddToLevelItemsDead(Id);
		}

		public void Enable()
		{
			_item.SetActive(true);
		}


	}
}