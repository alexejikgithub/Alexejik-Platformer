using Platformer.UI.MainMenu;
using System.Collections;
using UnityEngine;

namespace Platformer.Utils
{
	public static class WindowUtils
	{
		public static void CreateWindow(string resoursePath)
		{
			var window = Resources.Load<GameObject>(resoursePath);
			var canvas = Object.FindObjectOfType<BaseCanvas>();
			
			Object.Instantiate(window, canvas.transform);
		}
		
	}
}