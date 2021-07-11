using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Platformer.Creatures.Mobs.Boss
{
	public class ChangeLightsComponent : MonoBehaviour
	{
		[SerializeField] private Light2D[] _lights;

		[ColorUsage(true, true)]
		[SerializeField] private Color _color;


		[ContextMenu("Setup")]
		public void SetColor()
		{
			foreach( var light2D in _lights)
			{
				light2D.color = _color;
			}
		}
	}

}
