
using System.Collections.Generic;

using UnityEngine;
using Platformer.Utils;
using System;
using UnityEngine.Events;
using System.Linq;

namespace Platformer.Components.ColliderBased

{
	public class CheckCircleOverlap : MonoBehaviour
	{
		[SerializeField] private float _radius = 1f;
		[SerializeField] private LayerMask _mask;
		[SerializeField] private string[] _tags;
		[SerializeField] private OnOverlapEvent _onOverlap;
 		private Collider2D[] _interactResult = new Collider2D[10];


#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			UnityEditor.Handles.color = HandlesUtils.TransparentRed;
			UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
		}
#endif
		public void Check()
		{
			var size = Physics2D.OverlapCircleNonAlloc(
				transform.position,
				_radius,
				_interactResult,_mask);

			var overlaps = new List<GameObject>();
			for (int i = 0; i < size; i++)
			{
				var overlapResult = _interactResult[i];
				var isInTags = _tags.Any(tag => overlapResult.CompareTag(tag));
				if(isInTags)
				{
					_onOverlap?.Invoke(overlapResult.gameObject);
				}
				
				
			}
		}
		[Serializable]
		public class OnOverlapEvent : UnityEvent<GameObject>
		{

		}
	}
}