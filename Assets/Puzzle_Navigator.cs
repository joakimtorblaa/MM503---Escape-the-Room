using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Navigator : MonoBehaviour {


//Primary navigation

//Sub navigation

public GameObject pointerNav;

public GameObject activeObj;

int selectionX, puzzleSelX1, puzzleSelX2, puzzleSelX3, puzzleSelX4;
int selectionY, puzzleSelY1, puzzleSelY2, puzzleSelY3, puzzleSelY4;
string[,] mainPuzzles = new string[,]{
	{("Box021"),("Box020")},
	{("Box019"),("Box018")},
};
string[,] puzzle1Nav = new string[,]{
	{("Box028"), ("Box027"), ("Box026")},
	{("Box031"), ("Box030"), ("Box029")},
	{("Box034"), ("Box033"), ("Box032")},
};

string[,] puzzle4Nav = new string[,]{
	{("Box038"),("Box040")},
	{("Box039"),("Box041")},
};
/*Vector3[,] puzzle1Nav = new Vector3[,]{
			{new Vector3(0.95f,1.65f,-1.5f),new Vector3(0.95f,1.60f,-1.5f),new Vector3(0.95f,1.55f,-1.5f)},
			{new Vector3(0.9f,1.65f,-1.5f),new Vector3(0.9f,1.60f,-1.5f),new Vector3(0.9f,1.55f,-1.5f)},
			{new Vector3(0.85f,1.65f,-1.5f),new Vector3(0.85f,1.60f,-1.5f),new Vector3(0.85f,1.55f,-1.5f)},
};*/

public List<string> puzzle2Nav;

public List<string> puzzle3Nav;

bool subPuzzleLock;
bool puzzle1Lock;
bool puzzle2Lock;
bool puzzle3Lock;
bool puzzle4Lock;

	void Start(){

		pointerNav = GameObject.Find("Capsule");
		
		puzzle2Nav = new List<string>();
		puzzle2Nav.Add("Cylinder005");
		puzzle2Nav.Add("Cylinder006");
		puzzle2Nav.Add("Cylinder007");
		puzzle2Nav.Add("Cylinder008");
		puzzle2Nav.Add("Cylinder009");

		puzzle3Nav = new List<string>();
		puzzle3Nav.Add("Cylinder010");
		puzzle3Nav.Add("Cylinder011");
		puzzle3Nav.Add("Cylinder012");
		puzzle3Nav.Add("Cylinder013");
		puzzle3Nav.Add("Cylinder014");

		
		//General navigation
		selectionX = 0;
		selectionY = 0;
		//Puzzle 1 nav

		//Puzzle 2 nav
		puzzleSelX2 = 0;
		puzzleSelY2 = 0;
		//Puzzle 3 nav

		//Puzzle 4 nav


		subPuzzleLock = false;
		activeObj = GameObject.Find(mainPuzzles[selectionX, selectionY]);
		pointerNav.transform.position = activeObj.transform.position;
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

				activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
				pointerNav.transform.position = activeObj.transform.position;
					
			} else if(horisontal != 0){
				selectionX += horisontal;

				activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
				pointerNav.transform.position = activeObj.transform.position;
			}

		} else if (subPuzzleLock == true && puzzle1Lock == true){
			//stop outofrangeY (not errormessage)
			puzzleSelY1 = (puzzleSelY1 >= 3) ? 2 : (puzzleSelY1 < 0) ? 0: puzzleSelY1;

			//stop outofrangeX (not errormessage)
			puzzleSelX1 = (puzzleSelX1 >= 3) ? 2 : (puzzleSelX1 < 0) ? 0: puzzleSelX1;
			if(vertical != 0){
				puzzleSelY1 += vertical;

				activeObj = GameObject.Find(puzzle1Nav[puzzleSelX1,puzzleSelY1]);
				pointerNav.transform.position = activeObj.transform.position;
					
			} else if(horisontal != 0){
				puzzleSelX1 += horisontal;

				activeObj = GameObject.Find(puzzle1Nav[puzzleSelX1,puzzleSelY1]);
				pointerNav.transform.position = activeObj.transform.position;
			}

			if(Input.GetKeyDown(KeyCode.Space)){
				
				Renderer rend = activeObj.GetComponent<Renderer>();
				rend.material.SetColor("_Color", Color.red);
			}
		} else if (subPuzzleLock == true && puzzle2Lock == true){

			//stop outofrangeX (not errormessage)
			puzzleSelX2 = (puzzleSelX2 >= 5) ? 4 : (puzzleSelX2 < 0) ? 0: puzzleSelX2;
			if(horisontal != 0){
				puzzleSelX2 += horisontal;
				activeObj = GameObject.Find(puzzle2Nav[puzzleSelX2]);
				pointerNav.transform.position = activeObj.transform.position;
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				
				if(activeObj.transform.rotation.eulerAngles.x == 300){
					activeObj.transform.Rotate(Vector3.right, 120);
					
				} else {
					activeObj.transform.Rotate(Vector3.right, -120);
		
				}
			}

		} else if (subPuzzleLock == true && puzzle3Lock == true){
			//stop outofrangeX (not errormessage)
			puzzleSelX3 = (puzzleSelX3 >= 5) ? 4 : (puzzleSelX3 < 0) ? 0: puzzleSelX3;
			if(horisontal != 0){
				puzzleSelX3 += horisontal;
				activeObj = GameObject.Find(puzzle3Nav[puzzleSelX3]);
				pointerNav.transform.position = activeObj.transform.position;
			}
			if(Input.GetKeyDown(KeyCode.Space) && puzzle3Nav.IndexOf(puzzle3Nav[puzzleSelX3]) != 4){
				activeObj.transform.Rotate(Vector3.forward, 20);
					

			} else if (Input.GetKeyDown(KeyCode.Space)){
				if(activeObj.transform.rotation.eulerAngles.x == 300){
					activeObj.transform.Rotate(Vector3.right, 120);
					
				} else {
					activeObj.transform.Rotate(Vector3.right, -120);
		
				}
			}
		} else if (subPuzzleLock == true && puzzle4Lock == true){
			//stop outofrangeY (not errormessage)
			puzzleSelY4 = (puzzleSelY4 >= 2) ? 1 : (puzzleSelY4 < 0) ? 0: puzzleSelY4;

			//stop outofrangeX (not errormessage)
			puzzleSelX4 = (puzzleSelX4 >= 2) ? 1 : (puzzleSelX4 < 0) ? 0: puzzleSelX4;
			if(vertical != 0){
				puzzleSelY4 += vertical;
				
				activeObj = GameObject.Find(puzzle4Nav[puzzleSelX4,puzzleSelY4]);
				pointerNav.transform.position = activeObj.transform.position;
					
			} else if(horisontal != 0){
				puzzleSelX4 += horisontal;
				
				activeObj = GameObject.Find(puzzle4Nav[puzzleSelX4,puzzleSelY4]);
				pointerNav.transform.position = activeObj.transform.position;
			}
		}


		if(Input.GetKeyDown(KeyCode.Space) && subPuzzleLock == false){
			
			if(selectionX == 0 && selectionY == 0){
				subPuzzleLock = true;
				puzzle1Lock = true;
				activeObj = GameObject.Find(puzzle1Nav[puzzleSelX1,puzzleSelY1]);
				pointerNav.transform.position = activeObj.transform.position;
			} else if (selectionX == 0 && selectionY == 1) {
				subPuzzleLock = true;
				puzzle2Lock = true;
				Debug.Log("Puzzle2");
				activeObj = GameObject.Find(puzzle2Nav[puzzleSelX2]);
				pointerNav.transform.position = activeObj.transform.position;
			} else if (selectionX == 1 && selectionY == 0) {
				subPuzzleLock = true;
				puzzle3Lock = true;
				Debug.Log("Puzzle3");
				activeObj = GameObject.Find(puzzle3Nav[puzzleSelX3]);
				pointerNav.transform.position = activeObj.transform.position;

			} else if (selectionX == 1 && selectionY == 1) {
				subPuzzleLock = true;
				puzzle4Lock = true;
				Debug.Log("Puzzle4");
				activeObj = GameObject.Find(puzzle4Nav[puzzleSelX4,puzzleSelY4]);
				pointerNav.transform.position = activeObj.transform.position;
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape) && subPuzzleLock == true){
			subPuzzleLock = false;
			puzzle1Lock = false;
			puzzle2Lock = false;
			puzzle3Lock = false;
			puzzle4Lock = false;
			activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
			pointerNav.transform.position = activeObj.transform.position;
		}


	}
	

}
