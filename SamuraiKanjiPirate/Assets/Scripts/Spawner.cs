using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	public float spawnTime1 = 2f;
	public float spawnDelay1 = 4f;
	public float spawnTime2 = 1f;
	public float spawnDelay2 = 3f;
	public static ArrayList targetted = new ArrayList ();
	public Kanji[] kanjis;		
	public Kanji target;
	public bool isHard;

	void Start ()
	{
		//resetPlayed ();
		resetTargetted();
		Choose ();
		InvokeRepeating("SpawnChosen", 1f, 5f);
		InvokeRepeating("Spawn", spawnDelay1, spawnTime1);
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
			if (isHard) {
				k.setIsHard (true);
			} else {
				k.setIsHard (false);
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
		disableAllinArray ();
		target = kanjis [Random.Range (0, kanjis.Length)];
		targetted.Add (target);
		string str = target.GetComponent<Kanji> ().getMeaning ();
		GameObject.FindGameObjectWithTag ("kanji").GetComponent<TextUpdate> ().SetText (str);
		StartCoroutine(setAll ());
	}

	void SpawnChosen() {
		Instantiate(target,new Vector3(Random.Range(-7,7),7,0),transform.rotation);
	}

	void resetTargetted() {
		targetted.Clear ();
	}

	void Spawn ()
	{
		int kanjisIndex = Random.Range(0, kanjis.Length);
		Instantiate(kanjis[kanjisIndex],new Vector3(Random.Range(-7,7),7,0),transform.rotation);
	}
}
