using System.Collections;
using UnityEngine;

namespace Platformer.Components.ColliderBased
{
	public class LayerCheck : MonoBehaviour
	{

		[SerializeField] protected LayerMask Layer;
		[SerializeField] protected bool _isTouchingLayer;

		public bool IsTouchingLayer => _isTouchingLayer;
	}
}