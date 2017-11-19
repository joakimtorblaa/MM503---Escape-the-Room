using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneChanger : MonoBehaviour {

	private GameObject storeSelected;

	void Update (){
			if(EventSystem.current.currentSelectedGameObject == null){
				EventSystem.current.SetSelectedGameObject(storeSelected);
			}
			if(Cursor.visible == false){
				Cursor.visible = true;
			}
	}

	void LateUpdate (){
		storeSelected = EventSystem.current.currentSelectedGameObject;
	}

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
