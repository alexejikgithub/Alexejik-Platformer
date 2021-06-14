using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitAndDoAction : MonoBehaviour
{
	[SerializeField] float _secondsToWait;
	[SerializeField] private UnityEvent _doAction;
	public UnityEvent DoAction => _doAction;

	public IEnumerator WaitAndAction()
	{
		yield return new WaitForSeconds(_secondsToWait);
		DoAction?.Invoke();
	}

	public void DoWaitAndAction()
	{
		StartCoroutine(WaitAndAction());
	}
}
