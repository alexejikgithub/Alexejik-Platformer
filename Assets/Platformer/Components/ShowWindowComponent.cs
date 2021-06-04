using Platformer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Components
{
	public class ShowWindowComponent: MonoBehaviour
	{
		[SerializeField] private string _path;

		public void Show()
		{
			WindowUtils.CreateWindow(_path);
		}

	}
}
