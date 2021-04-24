using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer
{
	public class TeleportComponent : MonoBehaviour
	{
		[SerializeField] private Transform _destTransform;

		[SerializeField] private float _alphaTime = 1;
		[SerializeField] private float _moveTime = 1;

		public void Teleport(GameObject target)
		{
			//target.transform.position = _destTransform.position;
			StartCoroutine(AnimateTeleport(target));

		}

		private IEnumerator AnimateTeleport(GameObject target)
		{
			var sprite = target.GetComponent<SpriteRenderer>();
			var input = target.GetComponent<PlayerInput>();

			SetLockInput(input, true);

			yield return AlphaAnimation(sprite, 0);
			target.SetActive(false);

			yield return MoveAnimation(target);

			target.SetActive(true);
			yield return AlphaAnimation(sprite, 1);


			SetLockInput(input, false);

		}

		private void SetLockInput(PlayerInput input, bool isLocked)
		{
			
			if (input != null)
			{
				input.enabled = !isLocked;
			}
		}

		private IEnumerator MoveAnimation(GameObject target)
		{
			var moveTime = 0f;
			var startPosition = target.transform.position;
			while (moveTime < _moveTime)
			{
				moveTime += Time.deltaTime;
				var progress = moveTime / _moveTime;
				target.transform.position = Vector3.Lerp(startPosition, _destTransform.position, progress);
				yield return null;
			}
		}

		private IEnumerator AlphaAnimation(SpriteRenderer sprite, float destAlpha)
		{
			var time = 0f;

			var spriteAlfa = sprite.color.a;
			while (time < _alphaTime)
			{
				time += Time.deltaTime;
				var progress = time / _alphaTime;
				var tmpAlpha = Mathf.Lerp(spriteAlfa, destAlpha, progress);
				var color = sprite.color;
				color.a = tmpAlpha;
				sprite.color = color;

				yield return null;

			}
		}
	}

}
