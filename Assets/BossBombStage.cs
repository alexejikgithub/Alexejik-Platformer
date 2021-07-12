using Platformer.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBombStage : StateMachineBehaviour
{
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var spawner = animator.GetComponentInChildren<ProbabilityDropComponent>();
		spawner.CalculateDrop();
	}
}
