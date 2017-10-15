using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {


	void Start () {
		Debug.Log ("made it");
		foreach (Kanji k in Spawner.targetted) {
			//Instantiate(k,0,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
