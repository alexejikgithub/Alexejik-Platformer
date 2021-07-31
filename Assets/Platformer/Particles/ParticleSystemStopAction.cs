using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Particles
{
	public class ParticleSystemStopAction : MonoBehaviour
	{
		[SerializeField] UnityEvent _event;

		private void OnParticleSystemStopped()
		{
			_event?.Invoke();
		}
	}
}
