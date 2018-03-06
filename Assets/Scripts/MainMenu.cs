using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public GameObject loadingScreen;
	public string sceneName;
	public Slider slider;


	void Start(){
		if (GameObject.Find ("FungusManager")) {
			Destroy (GameObject.Find ("FungusManager"));
		}
	}


	public void PlayGame(){
		StartCoroutine (LoadAsynchronously (sceneName));
	}

	// Show and handle Loading Bar
	IEnumerator LoadAsynchronously (string scene){
		AsyncOperation operation = SceneManager.LoadSceneAsync (scene);
		loadingScreen.SetActive (true);
		while (!operation.isDone){
			float progess = Mathf.Clamp01 (operation.progress / 0.9f);
			slider.value = progess;
			yield return null;
		}
	}
		
	public void QuitGame(){
		Application.Quit ();
	}
}
