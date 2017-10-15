using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
	public int score = 0;					// The player's score.


	//private PlayerControl playerControl;	// Reference to the player control script.
	//private int previousScore = 0;			// The score in the previous frame.


	//void Awake ()
	//{
		// Setting up the reference.
	//	playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	//}

	public void increaseScore() {
		score++;
		if(score == 2) {
			SceneManager.LoadScene ("Win");
		}
	}

	void Update ()
	{
		// Set the score text.
		GetComponent<GUIText>().text = "Score: " + score;

		// If the score has changed...
		//if(previousScore != score)
			// ... play a taunt.
			//playerControl.StartCoroutine(playerControl.Taunt());

		// Set the previous score to this frame's score.
		//previousScore = score;
	}

}
