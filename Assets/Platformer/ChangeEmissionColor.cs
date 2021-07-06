using System.Collections;
using UnityEngine;

namespace Platformer
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class ChangeEmissionColor : MonoBehaviour
	{

		[ColorUsage(true, true)]
		[SerializeField]
		private Color _color;

		private static readonly int EmissionColor = Shader.PropertyToID("_Color");

		private SpriteRenderer _sprite;

		private void Awake()
		{
			_sprite = GetComponent<SpriteRenderer>();
		}

		[ContextMenu("ChangeColor")]
		public void Change()
		{
			_sprite.material.SetColor(EmissionColor, _color);
		}
	}
}