using UnityEngine;
using System.Collections;

public class Kanji : MonoBehaviour
{

	public bool isSelected;
	public Transform trans;
	public GameObject explosion;
	public string meaning;
	public BoxCollider2D collider;

	void FixedUpdate ()
	{
		offScreen ();
	}

	private void offScreen() 
	{
		trans = GetComponent<Transform> ();
		if(trans.position.y < -10) 
		{
			Destroy (gameObject);
		}
	}
	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Ground") {
			if (GameObject.FindGameObjectWithTag ("life1") != null) {
				GameObject.FindGameObjectWithTag ("life1").GetComponent<Heart> ().Destroy ();
			} else if(GameObject.FindGameObjectWithTag ("life2") != null) {
				GameObject.FindGameObjectWithTag ("life2").GetComponent<Heart> ().Destroy ();
			} else if(GameObject.FindGameObjectWithTag ("life3") != null) {
				GameObject.FindGameObjectWithTag ("life3").GetComponent<Heart> ().Destroy ();
				DisableCollider();
				Application.LoadLevel ("GameOver");
			}
		}
	}

	public string getMeaning() {
		return meaning;
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "sword") {
			GameObject.FindGameObjectWithTag ("score").GetComponent<Score> ().increaseScore ();
			Instantiate (explosion, trans.position, transform.rotation = Quaternion.identity);
			Destroy (gameObject);
		}
	}

	public void EnableCollider() {
		collider = GetComponent<BoxCollider2D> ();
		collider.enabled = true;
	}

	public void DisableCollider() {
		collider = GetComponent<BoxCollider2D> ();
		collider.enabled = false;
	}
}