using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Platformer.Creatures.Mobs.ShootingTraps

{
	[ExecuteInEditMode]
	public class TotemConstructor : MonoBehaviour
	{

		[SerializeField] private GameObject _totemHead;

		[SerializeField] private int _numOfHeads = 0;
		private float _yOffsetLength = -0.685f;

		[SerializeField] private List<TotemHead> _heads;

		public List<TotemHead> ReturnListOfHeads()
		{
			return _heads;
		}

		

#if UNITY_EDITOR
			private void Update()
		{
			
			while (_heads.Count < _numOfHeads)
			{
				int steps = _heads.Count;
				float yOffset = _yOffsetLength * (steps - 1);
				var instantiate = Instantiate(_totemHead, new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), Quaternion.identity);
				instantiate.transform.parent = gameObject.transform;

				_heads.Add(instantiate.GetComponent<TotemHead>());

			}

			while (_heads.Count > _numOfHeads)
			{

				var destroyable = _heads[_heads.Count - 1].gameObject;
				_heads.RemoveAt(_heads.Count - 1);
				if (destroyable != null)
				{
					DestroyImmediate(destroyable);
				}

			}



		}
	}
#endif
}