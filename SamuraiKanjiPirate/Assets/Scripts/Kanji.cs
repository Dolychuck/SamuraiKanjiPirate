using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Kanji : MonoBehaviour
{

	public bool isSelected;
	public Transform trans;
	public GameObject explosion;
	public string meaning;
	public BoxCollider2D collider;
	public EdgeCollider2D edgeCollider;
	public Rigidbody2D body;
	public bool isDestroyed;
	public bool isGrounded;
	public int playCount;
	public bool isHard;

	void start() {
		playCount = 0;
	}
	public void setIsHard(bool dif) {
		isHard = dif;
	}

	public void incrementPlayCount() {
		playCount++;
	}

	public int getPlayCount() {
		return playCount;
	}

	public void setPlayCount(int playCount) {
		this.playCount = playCount;
	}

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
			isGrounded = true;
			body = GetComponent<Rigidbody2D> ();
			body.gravityScale = int.MaxValue;
			takeLife ();
		}
	}
	public bool getIsDestroyed() {
		return isDestroyed;
	}
	public string getMeaning() {
		return meaning;
	}

	/*IEnumerator Death() {
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene ("GameOver");
	} */

	void takeLife() 
	{
		if (GameObject.FindGameObjectWithTag ("life1") != null) {
			GameObject.FindGameObjectWithTag ("life1").GetComponent<Heart> ().Destroy ();
		} else if(GameObject.FindGameObjectWithTag ("life2") != null) {
			GameObject.FindGameObjectWithTag ("life2").GetComponent<Heart> ().Destroy ();
		} else if(GameObject.FindGameObjectWithTag ("life3") != null) {
			GameObject.FindGameObjectWithTag ("life3").GetComponent<Heart> ().Destroy ();
			SceneManager.LoadScene ("GameOver");
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(!isGrounded) {
			if (col.tag == "sword") {
				GameObject.FindGameObjectWithTag ("score").GetComponent<Score> ().increaseScore ();
				Instantiate (explosion, trans.position, transform.rotation = Quaternion.identity);
				isDestroyed = true;
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Spawner> ().Choose();
				incrementPlayCount ();
				Destroy (gameObject);
			}

			if (isHard == true) {
				if (col.name == "Ninja") {
					Instantiate (explosion, trans.position, transform.rotation = Quaternion.identity);
					Destroy (gameObject);
					takeLife ();
				}
			}

			if (col.name == "ninjaHead") {
				Instantiate (explosion, trans.position, transform.rotation = Quaternion.identity);
				Destroy (gameObject);
				takeLife ();
			}
		}
	}

	public void EnableCollider() {
		collider =  GetComponent<BoxCollider2D> ();
		collider.enabled = true;
	}

	public void DisableCollider() {
		collider =  GetComponent<BoxCollider2D> ();
		collider.enabled = false;
	}
}