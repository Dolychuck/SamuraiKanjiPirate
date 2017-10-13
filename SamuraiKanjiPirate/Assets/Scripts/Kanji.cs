using UnityEngine;
using System.Collections;

public class Kanji : MonoBehaviour
{

	public bool isSelected;
	public Transform tra;

	void FixedUpdate ()
	{
		offScreen ();
	}

	private void offScreen() 
	{
		tra = GetComponent<Transform> ();
		if(tra.position.y < -10) 
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		GameObject.FindGameObjectWithTag ("score").GetComponent<Score> ().increaseScore();
		Destroy (gameObject);
	}
}