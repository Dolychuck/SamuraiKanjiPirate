using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_move : MonoBehaviour {
	public float walkSpeed = 10f;
	public bool facingRight = true;
	public Animator anim;

	public void FixedUpdate() {
		float move = Input.GetAxis ("Horizontal");
		anim = GetComponent<Animator> ();

		if (move == 0) {
			anim.SetInteger ("State", 0);
		}
		if (move < 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (move * walkSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			anim.SetInteger ("State", 1);
		}
		if (move > 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (move * walkSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			anim.SetInteger ("State", 1);
		}

		if (move < 0 && facingRight) {
			Flip ();
		}

		if (move > 0 && !facingRight) {
			Flip ();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		transform.Rotate (Vector3.up * 180);
	}
}
