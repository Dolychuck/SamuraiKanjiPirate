using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime1 = 2f;
	public float spawnDelay1 = 3f;
	public float spawnTime2 = 1f;
	public float spawnDelay2 = 2f;	
	public GameObject[] kanjis;		

	void Start ()
	{
		InvokeRepeating("Spawn", spawnDelay1, spawnTime1);
		InvokeRepeating("Spawn", spawnDelay2, spawnTime2);
	}


	void Spawn ()
	{
		int kanjisIndex = Random.Range(0, kanjis.Length);
		Instantiate(kanjis[kanjisIndex],new Vector3(Random.Range(-7,7),7,0),transform.rotation);
	}
}
