using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer
{
	public class MyAnimationStates : MonoBehaviour
	{
		[SerializeField] public string _name;
		[SerializeField] public bool _loop;
		[SerializeField] public bool _allowNext;
		[SerializeField] public Sprite[] _sprites;
	}
}