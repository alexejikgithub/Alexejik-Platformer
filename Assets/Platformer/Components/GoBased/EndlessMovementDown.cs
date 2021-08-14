﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Components.GoBased
{
	


	
	public class EndlessMovementDown: MonoBehaviour
	{
		[SerializeField] private float _movingSpeed;
		[SerializeField] private float _startYPoint;
		[SerializeField] private float _finalYPoint;
		private Vector3 _vector = Vector3.down;
		private float _currentSpeed;

		

		private void FixedUpdate()
		{
			transform.position += _vector * Time.deltaTime * _currentSpeed;
			if (transform.position.y < _finalYPoint)
			{
				RelocateToStart();
			}
		}

		[ContextMenu("Move")]
		public void Move()
		{
			_currentSpeed = _movingSpeed;
		}

		[ContextMenu("Stop")]
		public void Stop()
		{
			_currentSpeed = 0;
		}

		[ContextMenu("Relocate")]
		public void RelocateToStart()
		{
			transform.position = new Vector3(transform.position.x, _startYPoint, transform.position.z);
		}


	}
}
