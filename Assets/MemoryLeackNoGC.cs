using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryLeackNoGC : MonoBehaviour
{
	[SerializeField] private Sprite sprite;

	private List<Sprite> _spites = new List<Sprite>();

	private void Update()
	{
		for (int i = 0; i < 100; i++)
		{
			_spites.Add(Instantiate(sprite));

		}

	}
}
