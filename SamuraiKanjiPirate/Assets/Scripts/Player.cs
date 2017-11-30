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

	public AudioClip landClip;
	public AudioClip jumpClip;

	[SerializeField]
	private LayerMask whatIsGround;
	private bool facingRight;
	private bool isAttacking;
	private Animator myAnimator;
	public int attackCount;
	public int jumpCount;
	public int jumpAttackCount;
	public int directionChangeCount;
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
		jumpCount = 0;
		attackCount = 0;
		jumpAttackCount = 0;
	}

	void Update() {
		HandleInput ();
	}

	public string getGrounded() {
		if (isGrounded) {
			return "Player Grounded";
		} else {
			return "Player not Grounded";
		}
	}


	public void play() {
		AudioSource audio = GetComponent<AudioSource> ();
		AudioSource.PlayClipAtPoint (audio.clip, myRigidBody.position);
	}

	public void play(AudioClip clip) {
		AudioSource.PlayClipAtPoint (clip, myRigidBody.position);
	}

	public int getJumpCount() {
		return jumpCount;
	}

	public int getAttackCount() {
		return attackCount;
	}

	public int getDirectionChangeCount() {
		return directionChangeCount;
	}

	public int getJumpAttackCount() {
		return jumpAttackCount;
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
		if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
			if (isGrounded)
				myRigidBody.velocity = new Vector2 (horizontal * movementSpeed, myRigidBody.velocity.y);
			else if(horizontal != 0) {
				myRigidBody.velocity = new Vector2 (horizontal * (movementSpeed/2), myRigidBody.velocity.y);
			}
		}
			
		if (isGrounded && jump) {
			isGrounded = false;
			myRigidBody.AddForce(new Vector2(0,jumpForce));
			myAnimator.SetTrigger ("jump");
			//play (jumpClip);
			jumpCount++;
		}
		myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
	}

	private void HandleAttacks() 
	{
		if (isAttacking && isGrounded) 
		{
			SwordCollider.enabled = false;
		}
		if (isAttacking && isGrounded &&!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) {
			myAnimator.SetTrigger ("attack");
			attackCount++;
			myRigidBody.velocity = Vector2.zero;
		}
		if (JumpAttack && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack")) {
			myAnimator.SetBool ("jumpAttack", true);
			jumpAttackCount++;
		}
		if (!JumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo (1).IsName ("JumpAttack")) {
			myAnimator.SetBool ("jumpAttack",false);
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
			directionChangeCount++;
		}
	}

	private void ResetValues() {
		isAttacking = false;
		jump = false;
		JumpAttack = false;
	}

	public bool IsGrounded() {
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
		SwordCollider.enabled = !SwordCollider.enabled;
	}

	public void AttackOn() {
		if (!SwordCollider.enabled)
			SwordCollider.enabled = true;
	}

	public void AttackOff() {
		if (SwordCollider.enabled)
			SwordCollider.enabled = false;
	}
}
