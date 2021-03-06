﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class JumpAttackBehavior : StateMachineBehaviour {

		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			animator.GetComponent<Player> ().AttackOn ();
		}

		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Player> ().AttackOff ();
		}
	}
