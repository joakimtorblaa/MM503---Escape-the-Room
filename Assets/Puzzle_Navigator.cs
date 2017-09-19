using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Navigator : MonoBehaviour {


//Primary navigation
public GameObject puzzle1;
public GameObject puzzle2;
public GameObject puzzle3;
public GameObject puzzle4;
//Sub navigation
public GameObject subPuzzle21;
public GameObject subPuzzle22;
public GameObject subPuzzle23;
public GameObject subPuzzle24;
public GameObject pointerNav;

int selectionX, puzzleSelX1, puzzleSelX2, puzzleSelX3, puzzleSelX4;
int selectionY, puzzleSelY1, puzzleSelY2, puzzleSelY3, puzzleSelY4;

bool subPuzzleLock;

	void Start(){
		puzzle1 = GameObject.Find("Puzzle1");
		puzzle2 = GameObject.Find("Puzzle2");
		puzzle3 = GameObject.Find("Puzzle3");
		puzzle4 = GameObject.Find("Puzzle4");
		subPuzzle21 = GameObject.Find("SubPuzzle1");
		subPuzzle22 = GameObject.Find("SubPuzzle2");
		subPuzzle23 = GameObject.Find("SubPuzzle3");
		subPuzzle24 = GameObject.Find("SubPuzzle4");
		pointerNav = GameObject.Find("Capsule");

		//General navigation
		selectionX = 1;
		selectionY = 1;
		//Puzzle 1 nav

		//Puzzle 2 nav
		puzzleSelX2 = 1;
		puzzleSelY2 = 1;
		//Puzzle 3 nav

		//Puzzle 4 nav


		subPuzzleLock = false;
		
		pointerNav.transform.position = puzzle1.transform.position;
	}

	void Update(){
		if(subPuzzleLock == false){
			//1,1 -> 1,2
			if(selectionX == 1 && selectionY == 1 && Input.GetKey(KeyCode.DownArrow)){
				selectionY = 2;
				pointerNav.transform.position = puzzle3.transform.position;
			}
			//1,2 -> 1,1
			if(selectionX == 1 && selectionY == 2 && Input.GetKey(KeyCode.UpArrow)){
				selectionY = 1;
				pointerNav.transform.position = puzzle1.transform.position;
			}
			//1,1 -> 2,1
			if(selectionX == 1 && selectionY == 1 && Input.GetKey(KeyCode.RightArrow)){
				selectionX = 2;
				pointerNav.transform.position = puzzle2.transform.position;
			}
			//2,1 -> 1,1
			if(selectionX == 2 && selectionY == 1 && Input.GetKey(KeyCode.LeftArrow)){
				selectionX = 1;
				pointerNav.transform.position = puzzle1.transform.position;
			}
			//1,2 -> 2,2
			if(selectionX == 1 && selectionY == 2 && Input.GetKey(KeyCode.RightArrow)){
				selectionX = 2;
				pointerNav.transform.position = puzzle4.transform.position;
			}
			//2,2 -> 1,2
			if(selectionX == 2 && selectionY == 2 && Input.GetKey(KeyCode.LeftArrow)){
				selectionX = 1;
				pointerNav.transform.position = puzzle3.transform.position;
			}
			//2,1 -> 2,2
			if(selectionX == 2 && selectionY == 1 && Input.GetKey(KeyCode.DownArrow)){
				selectionY = 2;
				pointerNav.transform.position = puzzle4.transform.position;
			}
			//2,2 -> 2,1
			if(selectionX == 2 && selectionY == 2 && Input.GetKey(KeyCode.UpArrow)){
				selectionY = 1;
				pointerNav.transform.position = puzzle2.transform.position;
			}
		}
		if(subPuzzleLock == false && selectionX == 2 && selectionY == 1 && Input.GetKey(KeyCode.Space)){
			//locks into the second puzzle
			subPuzzleLock = true;
			pointerNav.transform.position = subPuzzle21.transform.position;
			puzzleSelX2 = 1;
			puzzleSelY2 = 1;
		}
		if(subPuzzleLock == true && Input.GetKey(KeyCode.Escape)){
			subPuzzleLock = false;
			pointerNav.transform.position = puzzle2.transform.position;
		}
		if(subPuzzleLock == true){
			//1,1 -> 1,2
			if(puzzleSelX2 == 1 && puzzleSelY2 == 1 && Input.GetKey(KeyCode.DownArrow)){
				puzzleSelY2 = 2;
				pointerNav.transform.position = subPuzzle23.transform.position;
			}
			//1,2 -> 1,1
			if(puzzleSelX2 == 1 && puzzleSelY2 == 2 && Input.GetKey(KeyCode.UpArrow)){
				puzzleSelY2 = 1;
				pointerNav.transform.position = subPuzzle21.transform.position;
			}
			//1,1 -> 2,1
			if(puzzleSelX2 == 1 && puzzleSelY2 == 1 && Input.GetKey(KeyCode.RightArrow)){
				puzzleSelX2 = 2;
				pointerNav.transform.position = subPuzzle22.transform.position;
			}
			//2,1 -> 1,1
			if(puzzleSelX2 == 2 && puzzleSelY2 == 1 && Input.GetKey(KeyCode.LeftArrow)){
				puzzleSelX2 = 1;
				pointerNav.transform.position = subPuzzle21.transform.position;
			}
			//1,2 -> 2,2
			if(puzzleSelX2 == 1 && puzzleSelY2 == 2 && Input.GetKey(KeyCode.RightArrow)){
				puzzleSelX2 = 2;
				pointerNav.transform.position = subPuzzle24.transform.position;
			}
			//2,2 -> 1,2
			if(puzzleSelX2 == 2 && puzzleSelY2 == 2 && Input.GetKey(KeyCode.LeftArrow)){
				puzzleSelX2 = 1;
				pointerNav.transform.position = subPuzzle23.transform.position;
			}
			//2,1 -> 2,2
			if(puzzleSelX2 == 2 && puzzleSelY2 == 1 && Input.GetKey(KeyCode.DownArrow)){
				puzzleSelY2 = 2;
				pointerNav.transform.position = subPuzzle24.transform.position;
			}
			//2,2 -> 2,1
			if(puzzleSelX2 == 2 && puzzleSelY2 == 2 && Input.GetKey(KeyCode.UpArrow)){
				puzzleSelY2 = 1;
				pointerNav.transform.position = subPuzzle22.transform.position;
			}
		}
	}
	

}
