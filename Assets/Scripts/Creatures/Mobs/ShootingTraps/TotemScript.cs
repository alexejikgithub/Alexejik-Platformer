using Platformer.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs.ShootingTraps

{
	public class TotemScript : MonoBehaviour
	{
		[SerializeField] private TotemConstructor _totemConstructor;
		[SerializeField] private float _attackDelay = 0.5f;
		[SerializeField] private TotemHead _firstHead;
		[SerializeField] private Cooldown _rangeCooldown;
		private List<TotemHead> _heads;
		private void Start()
		{
			_heads = _totemConstructor.ReturnListOfHeads();
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
			if (_firstHead != null)
			{
				_firstHead?.RangeAttack();
				_rangeCooldown.Reset();
			}
			yield return new WaitForSeconds(_attackDelay);
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


	}
}