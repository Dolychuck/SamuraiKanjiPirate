using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_jump : MonoBehaviour {
	public float jumpForce = 150.0f;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public bool grounded = false;
	public LayerMask whatIsGround;
	public Animator anim;

	void FixedUpdate() {
		anim = GetComponent<Animator> ();
		bool jump = Input.GetButtonDown ("Jump");
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if(!grounded)
			anim.SetInteger ("State", 2);
		if (jump && grounded) {
			GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce);
		}
	}
}
