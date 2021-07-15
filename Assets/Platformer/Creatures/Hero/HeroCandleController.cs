using Platformer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Platformer.Creatures.Hero
{
	public class HeroCandleController : MonoBehaviour
	{
		[SerializeField] private GameObject _candle;
		[SerializeField] private Light2D _lightSourse;

		private GameSession _session;


		private float _nextActionTime = 0.0f;
		private float _consumptionPeriod = 1f;


		private bool _turnedOn => _candle.activeSelf;

		private void Start()
		{
			_session = FindObjectOfType<GameSession>();
		}
		void Update()
		{
			if (_turnedOn)
			{
				var currentFuel = _session.Data.Inventory.GetItem("CandleFuel");
				if (currentFuel == null)
				{
					TurnOf();
					return;
				}
				else
				{
					var progress = Mathf.Clamp(currentFuel.Value / 10f, 0, 1);
					
					_lightSourse.intensity = progress;

					// same as above
					//if (currentFuel.Value > 10)
					//{
					//	_lightSourse.intensity = 1;
					//}
					//else
					//{

					//	_lightSourse.intensity = currentFuel.Value / 10f;
					//}
				}

				DecreaseFuel();

			}

		}

		private void TurnOn()
		{
			_candle.SetActive(true);
		}

		private void TurnOf()
		{
			_candle.SetActive(false);
		}


		[ContextMenu("LightSwich")]
		public void SwichLight()
		{
			if (_turnedOn)
			{
				TurnOf();
			}
			else
			{
				TurnOn();
			}
		}

		//removes 1 CandleFuel EverySecond
		private void DecreaseFuel()
		{

			if (Time.time > _nextActionTime)
			{
				_nextActionTime = Time.time + _consumptionPeriod;
				_session.Data.Inventory.Remove("CandleFuel", 1);
			}
		}

	}
}
