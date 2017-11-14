using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public void StartGame(){
		StartCoroutine(ChangeLevel());
	}

	public void QuitGame(){
		Application.Quit();
	}
	
	IEnumerator ChangeLevel (){
		float fadeTime = GameObject.Find("MenuController").GetComponent<SceneFade>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene("EscapeTheRoom");
	}
}
