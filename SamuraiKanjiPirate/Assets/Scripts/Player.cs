using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D myRigidBody;

	[SerializeField]
	private float movementSpeed;
	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatIsGround;
	private bool facingRight;
	private bool isAttacking;
	private Animator myAnimator;
	public bool isGrounded;
	private bool jump;

	[SerializeField]
	private bool airControl;

	[SerializeField]
	private float jumpForce;
	void Start () {  
		facingRight = true;
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}

	void Update() {
		HandleInput ();
	}

	void FixedUpdate () 
	{
		isGrounded = IsGrounded ();
		float horizontal = Input.GetAxis ("Horizontal");
		HandleMovement(horizontal);
		flip (horizontal);
		HandleAttacks ();
		ResetValues ();
	}

	private void HandleMovement(float horizontal) 
	{
		if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") && isGrounded) {
			myRigidBody.velocity = new Vector2(horizontal*movementSpeed,myRigidBody.velocity.y);
		}

		if (isGrounded && jump) {
			isGrounded = false;
			myRigidBody.AddForce(new Vector2(0,jumpForce));
		}
		myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
	}

	private void HandleAttacks() 
	{
		if (isAttacking && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
			myAnimator.SetTrigger ("attack");
			myRigidBody.velocity = Vector2.zero;
		}
	}

	private void HandleInput() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			isAttacking = true;
		}
	}

	private void flip(float horizontal) 
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}

	private void ResetValues() {
		isAttacking = false;
		jump = false;
	}

	private bool IsGrounded() {
		if (myRigidBody.velocity.y <= 0) {
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++) {
					if (colliders[i].gameObject != gameObject) {
						return true;
					}
				}
			}
		}
		return false;
	}
}
