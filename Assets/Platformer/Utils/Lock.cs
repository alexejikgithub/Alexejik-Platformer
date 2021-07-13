using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Utils
{
	public class Lock
	{
		private readonly List<object> _retained = new List<object>();

		public void Retain(object item)
		{
			_retained.Add(item);
		}
		public void Release(object item)
		{
			_retained.Remove(item);
		}

		public bool IsLocked => _retained.Count > 0;
	}
}
