using Platformer.Creatures.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer.Components
{
	public class AliveSpiderCounter : MonoBehaviour
	{
		[SerializeField] private SpiderAI[] _spiders;
		
		public bool HaveChildren => CountAliveChildren()>0;

		public int CountAliveChildren()
		{
			int childrenCount=0;
			foreach (var spider in _spiders)
			{
				if(spider!=null && !spider.IsDead)
				{
					childrenCount += 1;
				}
				
			}
			
			return childrenCount;
		}

	}
}
