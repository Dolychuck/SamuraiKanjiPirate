using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextUpdate : MonoBehaviour {



	public void SetText (string str)
	{
		GetComponent<Text>().text = str;
	}
}
