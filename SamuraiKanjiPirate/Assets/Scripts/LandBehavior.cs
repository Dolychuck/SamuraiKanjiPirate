using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBehavior : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Player> ().AttackOff ();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Player> ().AttackOff ();
	}
}

