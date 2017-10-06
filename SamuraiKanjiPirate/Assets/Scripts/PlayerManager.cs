using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public float speedX;
	public float jumpSpeedY;

	bool jump;
	bool facingRight;
	float speed;

	Animator anim;
	Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		facingRight = true;
	}


	// Update is called once per frame
	void Update () {
		MovePlayer (speed);
		Flip ();

		//Move left
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			speed = -speedX;
		}

		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			speed = 0;
		}

		//Move right
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			speed = speedX;
		}

		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			speed = 0;
		}

		//Move up
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			jump = true;
			rb.AddForce (new Vector2 (rb.velocity.x,jumpSpeedY));
			anim.SetInteger ("State", 2);
		}
	}

	void Flip() {
		if(speed > 0 && !facingRight || speed < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 temp = transform.localScale;
			temp.x *= -1;
			transform.localScale = temp;
		}
	}

	void MovePlayer(float playerSpeed) {
		if (playerSpeed < 0 && jump == false || playerSpeed > 0 &&  jump == false) {
			anim.SetInteger ("State", 1);
		}
		if (playerSpeed == 0 && jump == false) {
			anim.SetInteger ("State", 0);
		}
		rb.velocity = new Vector3 (speed, rb.velocity.y, 0);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			jump = false;
			anim.SetInteger ("State", 0);
		}
	}

	public void walkLeft() {
		speed = -speedX;
	}
}
