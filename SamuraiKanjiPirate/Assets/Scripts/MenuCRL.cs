using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuCRL : MonoBehaviour {
	public void LoadScreen(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}
}
