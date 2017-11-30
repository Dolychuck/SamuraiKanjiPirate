using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public Player ninjaPlayer;
	public float spawnTime1 = 2f;
	public float spawnDelay1 = 4f;
	public float spawnTime2 = 0f;
	public float spawnDelay2 = 1f;
	public static ArrayList targetted = new ArrayList ();
	public Kanji[] kanjis;		
	public Kanji target;
	public bool isHard;
	public static System.Diagnostics.Stopwatch sw;
	public static int totalKanji;
	public bool isTutorial;

	void Start ()
	{
		Choose ();
		totalKanji = 0;
		sw = new System.Diagnostics.Stopwatch ();
		sw.Start ();
		resetTargetted();
		string difficulty;
		if (isHard) {
			difficulty = "hard";
		} else {
			difficulty = "easy";
		}
		/*
		FileWriter.WriteData ("************New Round ("+difficulty+")**************\n", "KanjiDeath.txt", "");
		FileWriter.WriteData ("************New Round ("+difficulty+")**************\n", "KanjiKilled.txt", "");
		FileWriter.WriteData ("************New Round ("+difficulty+")**************\n", "Movement.txt", "");
		FileWriter.WriteData ("************New Round ("+difficulty+")**************\n", "Round.txt", "");
		*/
		InvokeRepeating("Spawn", spawnDelay2, spawnTime2);
	}
		
	void disableAllinArray() {
		foreach(Kanji k in kanjis) {
			k.DisableCollider ();
		}
	}

	IEnumerator setAll() {
		yield return new WaitForSeconds (1);
		doSetAll ();
	}

	void doSetAll() {
		target.EnableCollider ();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
		foreach(GameObject e in enemies) {
			Kanji k = e.GetComponent<Kanji> ();
			if (k.meaning != target.meaning && !k.isGrounded) {
				k.DisableCollider ();
			} else {
				k.EnableCollider ();
			}
		}
	}
	public void resetPlayed() 
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
		foreach (GameObject e in enemies) {
			Kanji k = e.GetComponent<Kanji> ();
			k.setPlayCount (0);
		}
	}

	public void Choose() {
		if (kanjis.Length == 0)
			return;
		disableAllinArray ();
		target = kanjis [Random.Range (0, kanjis.Length)];
		targetted.Add (target);
		string str = target.GetComponent<Kanji> ().getMeaning ();
		GameObject.FindGameObjectWithTag ("kanji").GetComponent<TextUpdate> ().SetText (str);
		StartCoroutine(setAll ());
	}

	void resetTargetted() {
		targetted.Clear ();
	}

	void Spawn ()
	{
		if (kanjis.Length == 0)
			return;
		totalKanji++;
		if (totalKanji % 3 == 0) {
			Instantiate (target, new Vector3 (Random.Range (-7, 7), 7, 0), transform.rotation);
		} else {
			int kanjisIndex = Random.Range (0, kanjis.Length);
			if (isHard) {
				Instantiate (kanjis [kanjisIndex], new Vector3 (Random.Range (-7, 7), 7, 0), transform.rotation).setIsHard (true);
			} else {
				Instantiate (kanjis [kanjisIndex], new Vector3 (Random.Range (-7, 7), 7, 0), transform.rotation);
			}
		}
	}
}
