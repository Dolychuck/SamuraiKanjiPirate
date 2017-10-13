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
	private bool JumpAttack;
	[SerializeField]
	private bool airControl;

	[SerializeField]
	private EdgeCollider2D SwordCollider;

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
		HandleLayers ();
		ResetValues ();
	}

	private void HandleMovement(float horizontal) 
	{
		if (myRigidBody.velocity.y < 0) {
			myAnimator.SetBool("land",true);
		}
		if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") && isGrounded) {
			myRigidBody.velocity = new Vector2(horizontal*movementSpeed,myRigidBody.velocity.y);
		}

		if (isGrounded && jump) {
			isGrounded = false;
			myRigidBody.AddForce(new Vector2(0,jumpForce));
			myAnimator.SetTrigger ("jump");
		}
		myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
	}

	private void HandleAttacks() 
	{
		if (isAttacking && isGrounded &&!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
			myAnimator.SetTrigger ("attack");
			myRigidBody.velocity = Vector2.zero;
		}
		if (JumpAttack && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack")) {
			myAnimator.SetBool ("jumpAttack", true);
		}
		if (!JumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo (1).IsName ("JumpAttack")) {
			myAnimator.SetBool ("jumpAttack",false);
		}
		if (isAttacking && isGrounded && SwordCollider.enabled) {
			SwordCollider.enabled = !SwordCollider.enabled;
		}
	}

	private void HandleInput() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			isAttacking = true;
			JumpAttack = true;
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
		JumpAttack = false;
	}

	private bool IsGrounded() {
		if (myRigidBody.velocity.y <= 0) {
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++) {
					if (colliders[i].gameObject != gameObject) {
						myAnimator.ResetTrigger ("jump");
						myAnimator.SetBool("land",false);
						return true;
					}
				}
			}
		}
		return false;
	}

	private void HandleLayers() {
		if (!isGrounded) {
			myAnimator.SetLayerWeight (1, 1);
		} else {
			myAnimator.SetLayerWeight (1, 0);
		}
	}

	public void Attack() 
	{
		Debug.Log(SwordCollider.enabled = !SwordCollider.enabled);
	}

	public void AirAttack() {
		if (SwordCollider.enabled) {
			SwordCollider.enabled = !SwordCollider.enabled;
		}
	}
}
