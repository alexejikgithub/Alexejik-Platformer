using Platformer.Utils.Disposables;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Utils
{
	public static class UnityEventExtentions
	{
		public static IDisposable Subscribe(this UnityEvent unityEvent, UnityAction call)
		{
			unityEvent.AddListener(call);
			return new ActionDisposable(() => unityEvent.RemoveAllListeners());
		}

		public static IDisposable Subscribe<TType>(this UnityEvent<TType> unityEvent, UnityAction<TType> call)
		{
			unityEvent.AddListener(call);
			return new ActionDisposable(() => unityEvent.RemoveAllListeners());
		}

	}
}