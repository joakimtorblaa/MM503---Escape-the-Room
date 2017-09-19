using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Navigator : MonoBehaviour {

public Color startColor;
public Color highLightColor;
public GameObject puzzle1;
public GameObject puzzle2;
public GameObject puzzle3;
public GameObject puzzle4;

int selectionX;
int selectionY;

	void Start(){
		puzzle1 = GameObject.Find("Puzzle1");
		puzzle2 = GameObject.Find("Puzzle2");
		puzzle3 = GameObject.Find("Puzzle3");
		puzzle4 = GameObject.Find("Puzzle4");

		selectionX = 1;
		selectionY = 1;
		
	}

	void Update(){
		if(Input.GetKey(KeyCode.Space)){
			puzzle1.GetComponent<Renderer>().material.SetColor("_Color", startColor);
		}
		//1,1 -> 1,2
		if(selectionX == 1 && selectionY == 1 && Input.GetKey(KeyCode.DownArrow)){
			selectionY = 2;
			puzzle1.GetComponent<Renderer>().material.SetColor("_Color", startColor);
			puzzle3.GetComponent<Renderer>().material.SetColor("_Color", highLightColor);
		}
		//1,2 -> 1,1
		if(selectionX == 1 && selectionY == 2 && Input.GetKey(KeyCode.UpArrow)){
			selectionY = 1;
			puzzle3.GetComponent<Renderer>().material.SetColor("_Color", startColor);
			puzzle1.GetComponent<Renderer>().material.SetColor("_Color", highLightColor);
		}
		//1,1 -> 2,1
		if(selectionX == 1 && selectionY == 1 && Input.GetKey(KeyCode.RightArrow)){
			selectionX = 2;
			puzzle1.GetComponent<Renderer>().material.SetColor("_Color", startColor);
			puzzle2.GetComponent<Renderer>().material.SetColor("_Color", highLightColor);
		}
		//2,1 -> 1,1
		if(selectionX == 2 && selectionY == 1 && Input.GetKey(KeyCode.LeftArrow)){
			selectionX = 1;
			puzzle2.GetComponent<Renderer>().material.SetColor("_Color", startColor);
			puzzle1.GetComponent<Renderer>().material.SetColor("_Color", highLightColor);
		}
		//1,2 -> 2,2
		if(selectionX == 1 && selectionY == 2 && Input.GetKey(KeyCode.RightArrow)){
			selectionX = 2;
			puzzle3.GetComponent<Renderer>().material.SetColor("_Color", startColor);
			puzzle4.GetComponent<Renderer>().material.SetColor("_Color", highLightColor);
		}
		//2,2 -> 1,2
		if(selectionX == 2 && selectionY == 2 && Input.GetKey(KeyCode.LeftArrow)){
			selectionX = 1;
			puzzle3.GetComponent<Renderer>().material.SetColor("_Color", startColor);
			puzzle4.GetComponent<Renderer>().material.SetColor("_Color", highLightColor);
		}

	}
	

}
