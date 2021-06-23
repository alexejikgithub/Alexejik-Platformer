using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.UI.Windows.ShopWindow
{
	[RequireComponent (typeof(RectTransform))]
	public class WindowStartPositionUtil : MonoBehaviour
	{
		[SerializeField] float _startYPos;
		[SerializeField] RectTransform _reactTransform;

		private void Start()
		{
			_reactTransform.localPosition = new Vector2(0, _startYPos);
		}
	}
}
