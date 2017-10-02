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

//puzzle1 Nav, Input & Solutions
string[,] puzzle1Nav = new string[,]{
	{("Box028"), ("Box027"), ("Box026")},
	{("Box031"), ("Box030"), ("Box029")},
	{("Box034"), ("Box033"), ("Box032")},
};

string[,] puzzle1Input = new string[,]{
	{("1"), ("4"), ("7")},
	{("2"), ("5"), ("8")},
	{("3"), ("6"), ("9")},
};
public List<string> puzzle1Solutions;

//Puzzle2 Nav
public List<string> puzzle2Nav;
int[] slotValues = new int[]{
	1,1,1,1
};

//Puzzle3 Nav
public List<string> puzzle3Nav;

//Puzzle4 Nav
string[,] puzzle4Nav = new string[,]{
	{("Box038"),("Box040")},
	{("Box039"),("Box041")},
};

string puzzle1Code;
string puzzle1Solution;

//puzzlelocks
bool subPuzzleLock;
bool puzzle1Lock;
bool puzzle2Lock;
bool puzzle3Lock;
bool puzzle4Lock;

//puzzleSolved
bool puzzle1Solved;
bool puzzle2Solved;
bool puzzle3Solved;
bool puzzle4Solved;

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

		//Puzzle 1 solutions randomizer
		puzzle1Solutions = new List<string>();
		puzzle1Solutions.Add("3972");
		puzzle1Solutions.Add("5684");
		puzzle1Solutions.Add("1834");
		puzzle1Solutions.Add("8471");

		puzzle1Solution = puzzle1Solutions[Random.Range(0,3)];
		Debug.Log("Puzzle1Solution = " + puzzle1Solution);

		//General navigation
		selectionX = 0;
		selectionY = 0;

		//initial lock
		subPuzzleLock = false;

		//puzzle startphase
		puzzle1Solved = false;
		puzzle2Solved = false;
		puzzle3Solved = false;
		puzzle4Solved = false;

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
				puzzle1Code += puzzle1Input[puzzleSelX1,puzzleSelY1];
				Debug.Log(puzzle1Code);
				if(puzzle1Code.Length == 4){
					if(puzzle1Code == puzzle1Solution){
						Debug.Log("Puzzle solved!");
						puzzle1Solved = true;
						subPuzzleLock = false;
						puzzle1Lock = false;
						activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
						pointerNav.transform.position = activeObj.transform.position;
					} else {
						Debug.Log("Wrong code");
						puzzle1Code = "";
					}
				}
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
				if(slotValues[puzzleSelX3] < 9){
					slotValues[puzzleSelX3] += 1;
					Debug.Log(slotValues[puzzleSelX3] + ": value for slot " + puzzleSelX3);
				} else {
					Debug.Log("Back to one");
					slotValues[puzzleSelX3] = 1;
				}

			} else if (Input.GetKeyDown(KeyCode.Space)){
				if(activeObj.transform.rotation.eulerAngles.x == 300){
					activeObj.transform.Rotate(Vector3.right, 120);
					string p3Output = "";
					for(int i = 0;slotValues.Length > i; i++){
						p3Output = p3Output + slotValues[i].ToString();
					}
					if(p3Output == "1234"){
						Debug.Log("Puzzled solved!");
						puzzle3Solved = true;
						subPuzzleLock = false;
						puzzle3Lock = false;
						activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
						pointerNav.transform.position = activeObj.transform.position;
					} else {
						Debug.Log("Wrong input.");
					}
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

			if(Input.GetKeyDown(KeyCode.Space)){
				
				if(activeObj.transform.rotation.eulerAngles.x == 300){
					activeObj.transform.Rotate(Vector3.right, 120);
					
				} else {
					activeObj.transform.Rotate(Vector3.right, -120);
		
				}
			}
		}


		if(Input.GetKeyDown(KeyCode.Space) && subPuzzleLock == false){
			
			if(selectionX == 0 && selectionY == 0 && puzzle1Solved == false){
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
			} else if (selectionX == 1 && selectionY == 0 && puzzle3Solved == false) {
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
