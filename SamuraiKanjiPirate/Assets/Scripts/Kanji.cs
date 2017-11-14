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
	public Player Ninja;

	void start() {
		playCount = 0;
	}

	public void setIsHard(bool dif) {
		isHard = dif;
	}

	public bool getIsHard() {
		return isHard;
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
			takeLife (true);
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

	void takeLife(bool hitGround) 
	{
		string deathMethod;
		if (hitGround) {
			deathMethod = "Hit ground: ";
		} else {
			deathMethod = "Hit head: ";
		}
		Ninja = GameObject.FindGameObjectWithTag ("Ninja").GetComponent<Player>();
		if (GameObject.FindGameObjectWithTag ("life1") != null) {
			FileWriter.WriteData (deathMethod + meaning +"\n" + Ninja.getGrounded() + "\n", "KanjiDeath.txt", "");
			GameObject.FindGameObjectWithTag ("life1").GetComponent<Heart> ().Destroy ();
		} else if(GameObject.FindGameObjectWithTag ("life2") != null) {
			FileWriter.WriteData (deathMethod + meaning +"\n" + Ninja.getGrounded() + "\n", "KanjiDeath.txt", "");
			GameObject.FindGameObjectWithTag ("life2").GetComponent<Heart> ().Destroy ();
		} else if(GameObject.FindGameObjectWithTag ("life3") != null) {
			FileWriter.WriteData (deathMethod + meaning +"\n" + Ninja.getGrounded() + "\n", "KanjiDeath.txt", "");
			GameObject.FindGameObjectWithTag ("life3").GetComponent<Heart> ().Destroy ();
		} else if(GameObject.FindGameObjectWithTag ("life4") != null) {
			FileWriter.WriteData (deathMethod + meaning +"\n" + Ninja.getGrounded() + "\n", "KanjiDeath.txt", "");
			GameObject.FindGameObjectWithTag ("life4").GetComponent<Heart> ().Destroy ();
		} else if(GameObject.FindGameObjectWithTag ("life5") != null) {
			FileWriter.WriteData ("Result: Loss\nTotal time: " + Spawner.sw.ElapsedMilliseconds/1000  +
				"\nTotal Kanji: " + Spawner.totalKanji, "Round.txt", "");
			FileWriter.WriteData (deathMethod + meaning +"\n" + Ninja.getGrounded() + "\n", "KanjiDeath.txt", "");
			FileWriter.WriteData ("Jump Count: " + Ninja.getJumpCount() +"\nAttack Count: "
				+ Ninja.getAttackCount() + "\nJump_attack count: " 
				+ Ninja.getJumpAttackCount()+"\nDirection change count: " 
				+ Ninja.getDirectionChangeCount()+"\n", "Movement.txt", "");
			GameObject.FindGameObjectWithTag ("life5").GetComponent<Heart> ().Destroy ();
			SceneManager.LoadScene ("GameOver");
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		Ninja = GameObject.FindGameObjectWithTag ("Ninja").GetComponent<Player>();
		if(!isGrounded) {
			if (col.tag == "sword") {
				GameObject.FindGameObjectWithTag ("score").GetComponent<Score> ().increaseScore ();
				Instantiate (explosion, trans.position, transform.rotation = Quaternion.identity);
				isDestroyed = true;
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Spawner> ().Choose();
				FileWriter.WriteData ("Killed: " + meaning +"\n" + Ninja.getGrounded() + "\n", "KanjiKilled.txt", "");
				incrementPlayCount ();
				Destroy (gameObject);
			}

			if (getIsHard()) {
				if (col.tag == "Ninja") {
					Instantiate (explosion, trans.position, transform.rotation = Quaternion.identity);
					Destroy (gameObject);
					takeLife (false);
				}
			}

			if (col.name == "ninjaHead") {
				Instantiate (explosion, trans.position, transform.rotation = Quaternion.identity);
				Destroy (gameObject);
				takeLife (false);
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