using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Components;



namespace Platformer.Components.GoBased
{
	public class GoContainerComponent : MonoBehaviour
	{
		[SerializeField] private GameObject[] _gos;
		[SerializeField] private DropEvent _onDrop;

		[ContextMenu("Drop")]
		public void Drop()
		{
			_onDrop?.Invoke(_gos);
		}
	}
}