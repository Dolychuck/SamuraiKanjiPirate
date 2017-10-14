using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Spawner : MonoBehaviour
{
	public float spawnTime1 = 2f;
	public float spawnDelay1 = 3f;
	public float spawnTime2 = 1f;
	public float spawnDelay2 = 2f;
	public GameObject[] kanjis;		
	public GameObject target;

	void Start ()
	{
		Choose ();
		InvokeRepeating("Spawn", spawnDelay1, spawnTime1);
		InvokeRepeating("Spawn", spawnDelay2, spawnTime2);
	}

	void Choose() {
		target = kanjis [Random.Range (0, kanjis.Length)];
		target.GetComponent<Kanji> ().EnableCollider ();
		string str = target.GetComponent<Kanji> ().getMeaning ();
		GameObject.FindGameObjectWithTag ("kanji").GetComponent<TextUpdate> ().SetText (str);
	}

	void Spawn ()
	{
		int kanjisIndex = Random.Range(0, kanjis.Length);
		Instantiate(kanjis[kanjisIndex],new Vector3(Random.Range(-7,7),7,0),transform.rotation);
	}
}
