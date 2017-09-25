using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Navigator : MonoBehaviour {


//Primary navigation

//Sub navigation

public GameObject pointerNav;

int selectionX, puzzleSelX1, puzzleSelX2, puzzleSelX3, puzzleSelX4;
int selectionY, puzzleSelY1, puzzleSelY2, puzzleSelY3, puzzleSelY4;
Vector3[,] mainPuzzles = new Vector3[,]{
			{new Vector3(0.9f,1.6f,-1.5f), new Vector3(0.9f,0.98f,-1.5f)},
			{new Vector3(-0.9f,1.65f,-1.5f), new Vector3(-0.9f,0.98f,-1.5f)},
		};
Vector3[,] puzzle1Nav = new Vector3[,]{
			{new Vector3(0.95f,1.65f,-1.5f),new Vector3(0.95f,1.60f,-1.5f),new Vector3(0.95f,1.55f,-1.5f)},
			{new Vector3(0.9f,1.65f,-1.5f),new Vector3(0.9f,1.60f,-1.5f),new Vector3(0.9f,1.55f,-1.5f)},
			{new Vector3(0.85f,1.65f,-1.5f),new Vector3(0.85f,1.60f,-1.5f),new Vector3(0.85f,1.55f,-1.5f)},
};


bool subPuzzleLock;

	void Start(){
		
		pointerNav = GameObject.Find("Capsule");
		
		
		//General navigation
		selectionX = 0;
		selectionY = 0;
		//Puzzle 1 nav

		//Puzzle 2 nav
		puzzleSelX2 = 1;
		puzzleSelY2 = 1;
		//Puzzle 3 nav

		//Puzzle 4 nav


		subPuzzleLock = false;
		pointerNav.transform.position = mainPuzzles[selectionX,selectionY];
	}

	void Update(){

		int vertical = Input.GetKeyDown(KeyCode.DownArrow) ? 1: Input.GetKeyDown(KeyCode.UpArrow) ? -1:0;
		int horisontal = Input.GetKeyDown(KeyCode.RightArrow) ? 1: Input.GetKeyDown(KeyCode.LeftArrow) ? -1:0;

		if(subPuzzleLock == false){
			//stop outofrangeY (not errormessage)
			selectionY = (selectionY >= 2) ? 1 : (selectionY < 0) ? 0: selectionY;

			//stop outofrangeX (not errormessage)
			selectionX = (selectionX >= 2) ? 1 : (selectionX < 0) ? 0: selectionX;

			if(vertical != 0){
				selectionY += vertical;
				Debug.Log(selectionY);
				pointerNav.transform.position = mainPuzzles[selectionX,selectionY];
					
			} else if(horisontal != 0){
				selectionX += horisontal;
				Debug.Log(selectionX);
				pointerNav.transform.position = mainPuzzles[selectionX,selectionY];
			}

		} else if (subPuzzleLock == true){
			//stop outofrangeY (not errormessage)
			puzzleSelY1 = (puzzleSelY1 >= 3) ? 2 : (puzzleSelY1 < 0) ? 0: puzzleSelY1;

			//stop outofrangeX (not errormessage)
			puzzleSelX1 = (puzzleSelX1 >= 3) ? 2 : (puzzleSelX1 < 0) ? 0: puzzleSelX1;
			if(vertical != 0){
				puzzleSelY1 += vertical;
				Debug.Log(puzzleSelY1);
				pointerNav.transform.position = puzzle1Nav[puzzleSelX1,puzzleSelY1];
					
			} else if(horisontal != 0){
				puzzleSelX1 += horisontal;
				Debug.Log(puzzleSelX1);
				pointerNav.transform.position = puzzle1Nav[puzzleSelX1,puzzleSelY1];
			}
		}

		if(Input.GetKeyDown(KeyCode.Space) && subPuzzleLock == false){
			subPuzzleLock = true;
			pointerNav.transform.position = puzzle1Nav[0,0];
		}
		if(Input.GetKeyDown(KeyCode.Escape) && subPuzzleLock == true){
			subPuzzleLock = false;
			pointerNav.transform.position = mainPuzzles[selectionX,selectionY];
		}

	}
	

}
