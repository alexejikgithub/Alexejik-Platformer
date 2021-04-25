using System.Collections;
using UnityEngine;

namespace Platformer.Components.ColliderBased
{
	public class LineCheck : LayerCheck
	{
		[SerializeField] private Transform _target;
		private RaycastHit2D[] _result = new RaycastHit2D[1];

		private void Update()
		{
			_isTouchingLayer = Physics2D.LinecastNonAlloc(transform.position, _target.position, _result, Layer) > 0;
			
			
		}
#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			UnityEditor.Handles.DrawLine(transform.position, _target.position);
		}
#endif
	}
}