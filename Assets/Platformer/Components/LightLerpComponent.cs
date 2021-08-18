using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Platformer.Components
{
	public class LightLerpComponent: MonoBehaviour
	{
		[SerializeField] private float _intencity;
		[SerializeField] private Light2D _light;
		
		public IEnumerator LerpLightIntencity()
		{
			float lerp = 0f, duration = 1f;

			_light.intensity= 0;
			while (_light.intensity != _intencity)
			{
				lerp += Time.deltaTime / duration;

				_light.intensity = Mathf.Lerp(0, _intencity, lerp);
				yield return new WaitForSeconds(Time.deltaTime);
			}


		}
		public void TurnLightOn()
		{
			StartCoroutine(LerpLightIntencity());
		}
	}
}
