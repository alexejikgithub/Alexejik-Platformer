using Platformer.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs.ShootingTraps

{
	public class TotemScript : MonoBehaviour
	{
		
		[SerializeField] private float _attackDelay = 0.5f;
		
		[SerializeField] private Cooldown _rangeCooldown;
		[SerializeField] private List<TotemHead> _heads;
		

		public List<TotemHead> ReturnListOfHeads()
		{
			return _heads;
		}
		private void Awake()
		{
			for (int i=0; i<_heads.Count;i++)
			{
				_heads[i].StreachAndCenterCollider(_heads.Count - i);
				if(i>0)
				{
					_heads[i].GetComponent<BoxCollider2D>().enabled = false;
				}
			}
		}




		[ContextMenu("Attack")]
		public void Attack()
		{
			if (_rangeCooldown.IsReady)
			{
				StartCoroutine(DoAttack());
			}
		}

		private IEnumerator DoAttack()
		{
			
			foreach (var head in _heads)
			{
				if(head !=null)
				{
					head?.RangeAttack();
					_rangeCooldown.Reset();
					yield return new WaitForSeconds(_attackDelay);

				}
				
			}

			
		}
		public void TurnOnTopCollider()
		{
			if(_heads.Count>1)
			{
				_heads.RemoveAt(0);
				_heads[0].GetComponent<BoxCollider2D>().enabled = true;
			}
			
			
		}


		

		

	}
}