using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {
	public GameObject nBox;
	public GameObject ten;
	public Text nText;
	public string[] convo;
	public AudioClip[] clips;
	static int curr;
	public bool IsGuy;
	void Start () {
		IsGuy = true;
		convo = new string[9];
		convo [0] = "Ninja where are you going?";
		convo [1] = "I want to become a Samurai so \nI'm going to the Samurai castle!";
		convo [2] = "WHHHAT! You can't become a \nSamurai! You can't even read \nKanji can you!?!?";
		convo [3] = "Noo! I can read Kanji very well!";
		convo [4] = "Really? Well quick how do you\n say this Kanji?";
		convo [5] = "Ummm you say this kanji\n \"Hachi\"";
		convo [6] = "huuuuh? That's wrong you liar!\n You can't read Kanji AT ALL!";
		convo [7] = "You becoming a Samurai is\n IMPOSSIBLE!";
		convo [8] = "Be quiet! I'll show you I\ncan learn Kanji!";
	}

	public void play(AudioClip clip) {
		AudioSource.PlayClipAtPoint(clip,new Vector3 (0, 0, -10));
	}

	void Update () {
		if (Input.anyKeyDown) {
			if (curr == 4) {
				Instantiate (ten, new Vector3 (nBox.transform.position.x+6f, nBox.transform.position.y-4, nBox.transform.position.z), transform.rotation = Quaternion.identity);
			}
			if (!IsGuy && curr != 0) {
				nBox.transform.position = new Vector3 (nBox.transform.position.x+8f, nBox.transform.position.y, nBox.transform.position.z);
				IsGuy = true;
			} else if(IsGuy && curr != 0 && curr!= 7) {
				nBox.transform.position = new Vector3 (nBox.transform.position.x-8f, nBox.transform.position.y, nBox.transform.position.z);
				IsGuy = false;
			}
			play (clips [curr]);
			nText.text = convo[curr++];

		} else {


		}
	}
}
