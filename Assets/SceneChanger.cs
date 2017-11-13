using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public void StartGame(){
		StartCoroutine(ChangeLevel());
	}
	
	IEnumerator ChangeLevel (){
		float fadeTime = GameObject.Find("MenuController").GetComponent<SceneFade>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Debug.Log("Test");
		SceneManager.LoadScene("EscapeTheRoom");
	}
}
