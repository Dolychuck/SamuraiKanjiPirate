using UnityEngine;
using System.Collections;

public class Kanji : MonoBehaviour
{
	public float moveSpeed = 5f;	

	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D>().AddForce(Vector3.down * moveSpeed);
	}
}