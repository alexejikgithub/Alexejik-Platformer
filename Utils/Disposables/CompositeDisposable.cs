using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Utils.Disposables
{


	public class CompositeDisposable : IDisposable
	{
		private readonly List<IDisposable> _disposables = new List<IDisposable>();

		public void Retain(IDisposable Disposable)
		{
			_disposables.Add(Disposable);
		}
		public void Dispose()
		{
			foreach (var disposable in _disposables)
			{
				disposable.Dispose();
			}
			_disposables.Clear();
		}
	}
}