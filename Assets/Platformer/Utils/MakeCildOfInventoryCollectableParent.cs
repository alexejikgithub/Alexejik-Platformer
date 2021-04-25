using Platformer.Components.Collectables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCildOfInventoryCollectableParent : MonoBehaviour
{
	
	private void Awake()
	{
		var target = FindObjectOfType<InventoryCollectableParent>();
		transform.parent = target.transform;
	}
}
