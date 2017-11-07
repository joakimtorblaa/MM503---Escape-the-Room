﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Navigator : MonoBehaviour {


public GameObject pointerNav;

public GameObject activeObj;
public GameObject numKeys;
public GameObject pBarP4, pScreenP4;
public GameObject challengeComplete;

public GameObject wireC;

public Random rnd = new Random();
int selectionX, puzzleSelX1, puzzleSelX2, puzzleSelX3, puzzleSelX4;
int selectionY, puzzleSelY1, puzzleSelY2, puzzleSelY3, puzzleSelY4;
int vertical = 0;
int horisontal = 0;
bool axisDown = false;
bool axisUp = false;
bool axisDown2 = false;
bool axisUp2 = false;

string[,] mainPuzzles = new string[,]{
	{("Box021"),("Box020")},
	{("Box019"),("Box018")},
};

int[] yearOptions = new int[]{1954, 1784, 1892};

float timeLeft = 300.0f;
float minutes;
float seconds;
//Numberpad display
GameObject numberDisplay1, numberDisplay2, yearDisplay, timerDisplay;
Text displayNumber1, displayNumber2, displayYear, displayTimer;

//puzzle1 Nav, Input & Solutions
string[,] puzzle1Nav = new string[,]{
	{("Box028"), ("Box027"), ("Box026")},
	{("Box031"), ("Box030"), ("Box029")},
	{("Box034"), ("Box033"), ("Box032")},
};

string[,] puzzle1Input;

string[,] puzzle1InputVer1 = new string[,]{
	{("A"), ("D"), ("G")},
	{("B"), ("E"), ("H")},
	{("C"), ("F"), ("I")},
};
string[,] puzzle1InputVer2 = new string[,]{
	{("A"), ("D"), ("K")},
	{("B"), ("E"), ("L")},
	{("C"), ("J"), ("M")},
};
public List<string> puzzle1Solutions;

public List<string> puzzle1OldChar;
public List<string> puzzle1NewChar;

string puzzle1Code;
bool puzzle1Resetting = false;
string puzzle1Solution;


//Puzzle2 Nav, Input
public List<string> puzzle2Nav;
int[] leverValues = new int[]{
	0,0,0,0,0
};

int[] onLeverValues = new int[]{
	1,2,4,8,16
};

bool pState1, pState2 = false;

string[] wireColor = new string[]{"blue", "red"};

public List<int> puzzle2DaySolutions;
public List<int> puzzle2MonthSolutions;

public int p2Output = 0;
public int puzzle2DaySolution;
public int puzzle2MonthSolution;

//Puzzle3 Nav
public List<string> puzzle3Nav;
int[] slotValues = new int[]{
	0,0,0,0
};
string[] p3Lights = new string[]{
	"p3Light1","p3Light2","p3Light3"
};
Color[] p3LightColors = new Color[]{
	Color.red, Color.green, Color.blue
};
string[] p3LightColorSet = new string[]{
	"r", "g", "b"
};

string[] puzzle3Input = new string[]{
	"READY", "HELP", "HELLO", "MIDDLE", "RIGHT", "STOP", "NEXT", "BLANK", "NOTHING"
};
List<string> puzzle3SolIndex = new List<string>{
	"rgb","brg","grb","bgr","rbg","gbr"
};
string[] puzzle3Solutions = new string[]{
	"HELLONOTHINGMIDDLEREADY","HELPSTOPNEXTMIDDLE","NEXTREADYBLANKHELLO","MIDDLENEXTHELPSTOP","READYBLANKHELLORIGHT","BLANKHELLOREADYNEXT",
};

string puzzle3Solution;
bool leverAnimation = false;

//Puzzle4 Nav, Input & solution
string[,] puzzle4Nav = new string[,]{
	{("Box038"),("Box040")},
	{("Box039"),("Box041")},
};

string[,] puzzle4Input = new string [,]{
	{("A"),("X")},
	{("B"),("Y")},
};

string[] puzzle4ScreenVal = new string[]{
	"AA", "AB", "AX", "AY", "BA", "BB", "BX", "BY", "XA", "XB", "XX", "XY", "YA", "YB", "YX", "YY" 
};

string[,] puzzle4Solutions = new string[,]{
	{"Y", "B", "A", "X", "B", "X", "Y", "A", "X", "A", "B", "Y", "A", "Y", "X", "B"},
	{"A", "Y", "X", "B", "X", "A", "B", "Y", "B", "X", "Y", "A", "Y", "B", "A", "X"},
	{"X", "A", "B", "Y", "A", "Y", "X", "B", "Y", "B", "A", "X", "B", "X", "Y", "A"},
};

Color[] puzzle4Colors = new Color[]{
	Color.red, Color.green, Color.blue
};

string puzzle4TmpVal;

int puzzle4Progress;
int puzzle4Rand;
string puzzle4Solution;

//challengeseed
int p1Ver;
string p2Ver;
//puzzlelocks
bool subPuzzleLock;
bool puzzle1Lock, puzzle2Lock, puzzle3Lock, puzzle4Lock;

//puzzleSolved
bool puzzle1Solved, puzzle2Solved, puzzle3Solved, puzzle4Solved;

bool gameCompleted;


    void Start(){
		gameCompleted = false;
		
		puzzle4TmpVal = "";
		pointerNav = GameObject.Find("Capsule");
		pBarP4 = GameObject.Find("ProgressSprite");
		pScreenP4 = GameObject.Find("Puzzle4Screen");
		
		puzzle2Nav = new List<string>(new string[] {"Cylinder005","Cylinder006","Cylinder007","Cylinder008","Cylinder009","p2Button"});
		
		puzzle3Nav = new List<string>(new string[] {"Cylinder010","Cylinder011","Cylinder012","Cylinder013","Cylinder014"});

		//Challenge 1 solutions
		puzzle1Solutions = new List<string>(new string[]{"¤ΩѮǷ","ƕԖЖϖ","ѮƕΘΩ","Жᴥ¶¤", "ΩǷ¶Ѯ", "ϖ‽ƕᴥ"});

		puzzle1OldChar = new List<string>(new string[]{"A","B","C","D","E","F","G","H","I","J","K","L","M"});
		puzzle1NewChar = new List<string>(new string[]{"ƕ","Ж","Ω","¶","¤","Ѯ","Ƿ","‡","Θ","ϖ","Ԗ","ᴥ","‽"});
		//Challenge 2 input randomizer
		
		for(int i = 0; i < onLeverValues.Length; i++){
			
			int tmp = onLeverValues[i];
			int j = Random.Range(i, onLeverValues.Length);
			onLeverValues[i] = onLeverValues[j];
			onLeverValues[j] = tmp;
		}
		//1954, 1784, 1892
		//Challenge 2 solutions
		puzzle2DaySolutions = new List<int>(new int[]{27,13,19,30,15,5});	
		puzzle2MonthSolutions = new List<int>(new int[]{3,10,4,11,6,7});

		//pushes activeyear to display
		int activeYear = yearOptions[Random.Range(0,3)];

		yearDisplay = GameObject.Find("YearTxt");
				
		displayYear = yearDisplay.GetComponent<Text>();
		displayYear.text = activeYear.ToString();

		//generates challengesolutions
		seedGenerator(activeYear);
		
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

		//Keyboard navigation inputs
		vertical = Input.GetKeyDown(KeyCode.S) ? 1: Input.GetKeyDown(KeyCode.W) ? -1: 0;
		horisontal = Input.GetKeyDown(KeyCode.D) ? 1: Input.GetKeyDown(KeyCode.A) ? -1: 0;
		
		//Controller navigation inputs
		if(Input.GetAxisRaw("Vertical") < 0){
			if(!axisDown){
				vertical = 1;
				axisDown = true;
			}
			if(axisUp){
				vertical = 0;
				axisUp = false;
			}
		} else if (Input.GetAxisRaw("Vertical") > 0){
			if(axisDown){
				vertical = 0;
				axisDown = false;
			}
			if(!axisUp){
				vertical = -1;
				axisUp = true;
			}
		} else if (Input.GetAxisRaw("Vertical") == 0){
			if(axisUp){
				vertical = 0;
				axisUp = false;
			}
			if(axisDown){
				vertical = 0;
				axisDown = false;
			}
			
		}
		
		if(Input.GetAxisRaw("Horizontal") < 0){
			if(!axisDown2){
				horisontal = -1;;
				axisDown2 = true;
			}
			if(axisUp2){
				horisontal = 0;
				axisUp2 = false;
			}
		} else if (Input.GetAxisRaw("Horizontal") > 0){
			if(axisDown2){
				horisontal = 0;
				axisDown2 = false;
			}
			if(!axisUp2){
				horisontal = 1;
				axisUp2 = true;
			}
		} else if (Input.GetAxisRaw("Horizontal") == 0){
			if(axisUp2){
				horisontal = 0;
				axisUp2 = false;
			}
			if(axisDown2){
				horisontal = 0;
				axisDown2 = false;
			}
			
		}


		

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

			if(Input.GetButtonDown("Submit") && puzzle1Resetting == false){
				puzzle1Code += puzzle1NewChar[puzzle1OldChar.IndexOf(puzzle1Input[puzzleSelX1,puzzleSelY1])];
				
				Debug.Log(puzzle1Code);

				numberDisplay1 = GameObject.Find("Puzzle1Txt");
				
				displayNumber1 = numberDisplay1.GetComponent<Text>();
				displayNumber1.text = puzzle1Code;

				if(puzzle1Code.Length == 4){
					if(puzzle1Code == puzzle1Solution){
						Debug.Log("Challenge solved!");
						puzzle1Solved = true;
						subPuzzleLock = false;
						puzzle1Lock = false;
						activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
						pointerNav.transform.position = activeObj.transform.position;

						if(puzzle2Lock == false || puzzle3Lock == false || puzzle4Lock == false){
							timeLeft = timeLeft + 30;
						}
						
						completionMarker("Challenge_solved1");
					} else {
						Debug.Log("Wrong code");
						StartCoroutine(waitDisplay());
						
						puzzle1Code = "";
					}
				}
			}
		} else if (subPuzzleLock == true && puzzle2Lock == true){

			//stop outofrangeX (not errormessage)
			puzzleSelX2 = (puzzleSelX2 >= 6) ? 5 : (puzzleSelX2 < 0) ? 0: puzzleSelX2;
			if(horisontal != 0){
				puzzleSelX2 += horisontal;
				activeObj = GameObject.Find(puzzle2Nav[puzzleSelX2]);
				pointerNav.transform.position = activeObj.transform.position;
			}
			if(Input.GetButtonDown("Submit") && puzzle2Nav.IndexOf(puzzle2Nav[puzzleSelX2]) != 5){
				
				if(activeObj.transform.rotation.eulerAngles.x == 300){
					activeObj.transform.Rotate(Vector3.right, 120);
					leverValues[puzzleSelX2] = onLeverValues[puzzleSelX2];
					
				} else {
					activeObj.transform.Rotate(Vector3.right, -120);
					leverValues[puzzleSelX2] = 0;
				}
			
				p2Output = 0;
				for(int i = 0;leverValues.Length > i; i++){
						p2Output = p2Output + leverValues[i];
					}
				numberDisplay2 = GameObject.Find("Puzzle2Txt");
				
				displayNumber2 = numberDisplay2.GetComponent<Text>();
				displayNumber2.text = p2Output.ToString();
			}else if(Input.GetButtonDown("Submit")){

				if(pState1 == false && p2Output == puzzle2DaySolution){
						completionMarker("Challenge_2_p1");
						
						pState1 = true;
				
				} else if (pState1 == true && pState2 == false && p2Output == puzzle2MonthSolution){
						completionMarker("Challenge_2_p2");
						
						pState2 = true;
					
				} else {
					Debug.Log("Wrong input");
				}

				if(pState1 == true && pState2 == true){
					puzzle2Solved = true;
					subPuzzleLock = false;
					puzzle2Lock = false;
					activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
					pointerNav.transform.position = activeObj.transform.position;

					if(puzzle1Lock == false || puzzle3Lock == false || puzzle4Lock == false){
							timeLeft = timeLeft + 30;
					}
					completionMarker("Challenge_solved2");
				}
			}

		} else if (subPuzzleLock == true && puzzle3Lock == true){
			//stop outofrangeX (not errormessage)
			puzzleSelX3 = (puzzleSelX3 >= 5) ? 4 : (puzzleSelX3 < 0) ? 0: puzzleSelX3;
			if(horisontal != 0 && leverAnimation == false){
				puzzleSelX3 += horisontal;
				activeObj = GameObject.Find(puzzle3Nav[puzzleSelX3]);
				pointerNav.transform.position = activeObj.transform.position;
			}
			if(Input.GetButtonDown("Submit") && puzzle3Nav.IndexOf(puzzle3Nav[puzzleSelX3]) != 4){
				activeObj.transform.Rotate(Vector3.back, 40);
				if(slotValues[puzzleSelX3] < 8){
					slotValues[puzzleSelX3] += 1;
					Debug.Log(slotValues[puzzleSelX3] + ": value for slot " + puzzleSelX3);
				} else {
					Debug.Log("Back to zero");
					slotValues[puzzleSelX3] = 0;
				}

			} else if (Input.GetButtonDown("Submit")){
				if(activeObj.transform.rotation.eulerAngles.x == 300){
					//activeObj.transform.Rotate(Vector3.right, 120);
					StartCoroutine(leverAnimate());
					string p3Output = "";
					for(int i = 0;slotValues.Length > i; i++){
						p3Output = p3Output + puzzle3Input[slotValues[i]];
					}
					if(p3Output == puzzle3Solution){
						Debug.Log("Challenge solved!");
						puzzle3Solved = true;
						subPuzzleLock = false;
						puzzle3Lock = false;
						activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
						pointerNav.transform.position = activeObj.transform.position;

						if(puzzle1Lock == false || puzzle2Lock == false || puzzle4Lock == false){
							timeLeft = timeLeft + 30;
						}
						completionMarker("Challenge_solved3");
					} else {
						Debug.Log("Wrong input.");
					}
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

			if(Input.GetButtonDown("Submit")){
				
				if(puzzle4Input[puzzleSelX4,puzzleSelY4] == puzzle4Solutions[puzzle4Rand, System.Array.IndexOf(puzzle4ScreenVal, puzzle4TmpVal)]){
					puzzle4Progress += 1;
					
					
					if (puzzle4Progress == 1){
						pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar1");
					} else if (puzzle4Progress == 2){
						pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar2");
					} else if (puzzle4Progress == 3){
						pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar3");
					} else if (puzzle4Progress == 4){
						pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar4");
					} else if (puzzle4Progress == 5){
						pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar5");
						Debug.Log("Challenge Solved");
						puzzle4Solved = true;
						subPuzzleLock = false;
						puzzle4Lock = false;
						activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
						pointerNav.transform.position = activeObj.transform.position;
						
						if(puzzle1Lock == false || puzzle2Lock == false || puzzle3Lock == false){
							timeLeft = timeLeft + 30;
						}
						completionMarker("Challenge_solved4");
					}
				} else {
					pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar0");
					Debug.Log("Puzzle reset");
					puzzle4Progress = 0;
				}
				if(puzzle4Progress != 5){
				puzzle4Screen();
				}
			}
		}


		if(Input.GetButtonDown("Submit") && subPuzzleLock == false){
			
			if(selectionX == 0 && selectionY == 0 && puzzle1Solved == false){
				subPuzzleLock = true;
				puzzle1Lock = true;
				activeObj = GameObject.Find(puzzle1Nav[puzzleSelX1,puzzleSelY1]);
				pointerNav.transform.position = activeObj.transform.position;
			} else if (selectionX == 0 && selectionY == 1 && puzzle2Solved == false) {
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

			} else if (selectionX == 1 && selectionY == 1 && puzzle4Solved == false) {
				subPuzzleLock = true;
				puzzle4Lock = true;
				Debug.Log("Puzzle4");
				activeObj = GameObject.Find(puzzle4Nav[puzzleSelX4,puzzleSelY4]);
				pointerNav.transform.position = activeObj.transform.position;
			}
		}
		if(Input.GetButtonDown("Cancel") && subPuzzleLock == true){
			subPuzzleLock = false;
			puzzle1Lock = false;
			puzzle2Lock = false;
			puzzle3Lock = false;
			puzzle4Lock = false;
			activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
			pointerNav.transform.position = activeObj.transform.position;
		}

		if(puzzle1Solved == true && puzzle2Solved == true && puzzle3Solved == true && puzzle4Solved == true){
			GameObject finish1 = GameObject.Find("Cylinder015");
			finish1.GetComponent<Renderer>().enabled = false;
			GameObject finish2 = GameObject.Find("Cylinder017");
			finish2.GetComponent<Renderer>().enabled = false;
			GameObject finish3 = GameObject.Find("Cylinder018");
			finish3.GetComponent<Renderer>().enabled = false;
			GameObject finish4 = GameObject.Find("Cylinder019");
			finish4.GetComponent<Renderer>().enabled = false;
			gameCompleted = true;
		}
		if(timeLeft > 0 && gameCompleted == false){
		//Debug.Log("its tiem");
		timeLeft -= Time.deltaTime;
		minutes = Mathf.Floor(timeLeft / 60);
		seconds = timeLeft % 60;
		if(seconds > 59) seconds = 59;
		timerDisplay = GameObject.Find("TimerTxt");
		if(minutes < 0){
			minutes = 0;
			seconds = 0;
		}
		displayTimer = timerDisplay.GetComponent<Text>();
		displayTimer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
		} 
		else if(gameCompleted == true) {
			Debug.Log("You win! You escaped with " + minutes + " minutes and " + seconds + " seconds left on the clock!");
			
		} else {
			Debug.Log("You lose!");
		}
	}
	
	


	IEnumerator waitDisplay(){
		puzzle1Resetting = true;
		displayNumber1.text = puzzle1Code;
		yield return new WaitForSecondsRealtime(1);
		displayNumber1.text = "****";
		yield return new WaitForSecondsRealtime(1);
		displayNumber1.text = "";
		puzzle1Resetting = false;

	}

	IEnumerator leverAnimate(){
		leverAnimation = true;
		GameObject.Find("Cylinder014").transform.Rotate(Vector3.right, 120);
		
		yield return new WaitForSecondsRealtime(1);
		
		GameObject.Find("Cylinder014").transform.Rotate(Vector3.right, -120);
		leverAnimation = false;
		
	}

	public void seedGenerator(int year){
		//Challenge 1 input randomizer
		
		p1Ver = Random.Range(0,2);
		p2Ver = wireColor[Random.Range(0,2)];
		Debug.Log(p1Ver);
		Debug.Log(p2Ver);
		p1Ver += 1;
		puzzle1Initializer("puzzle1InputVer" + p1Ver);
		wireCol(p2Ver, "Wire");
		puzzle3Initializer();
		puzzle4Screen();
		if(year == 1954){
			//Challenge1
			if(p1Ver == 1){
				puzzle1Solution = puzzle1Solutions[4];
			} else {
				puzzle1Solution = puzzle1Solutions[5];
			}
			
			//Challenge2
			if(p2Ver == "blue"){
				puzzle2DaySolution =  puzzle2DaySolutions[5];
				puzzle2MonthSolution = puzzle2MonthSolutions[5];
			} else {
				puzzle2DaySolution =  puzzle2DaySolutions[2];
				puzzle2MonthSolution = puzzle2MonthSolutions[2];
			}
		} else if (year == 1784){
			//Challenge1
			if(p1Ver == 1){
				puzzle1Solution = puzzle1Solutions[0];
			} else {
				puzzle1Solution = puzzle1Solutions[1];
			}
			//Challenge2
			if(p2Ver == "blue"){
				puzzle2DaySolution =  puzzle2DaySolutions[3];
				puzzle2MonthSolution = puzzle2MonthSolutions[3];
			} else {
				puzzle2DaySolution =  puzzle2DaySolutions[0];
				puzzle2MonthSolution = puzzle2MonthSolutions[0];
			}
			
				
			
		} else {
			//Challenge1
			if(p1Ver == 1){
				puzzle1Solution = puzzle1Solutions[2];
			} else {
				puzzle1Solution = puzzle1Solutions[3];
			}
			
			//Challenge2
			if(p2Ver == "blue"){
				puzzle2DaySolution =  puzzle2DaySolutions[4];
				puzzle2MonthSolution = puzzle2MonthSolutions[4];
			} else {
				puzzle2DaySolution =  puzzle2DaySolutions[1];
				puzzle2MonthSolution = puzzle2MonthSolutions[1];
			}
		}

	}

	public void completionMarker(string gObject){
		challengeComplete = GameObject.Find(gObject);
		Renderer rend = challengeComplete.GetComponent<Renderer>();
		rend.material.SetColor("_Color", Color.green);
			
	}

	public void wireCol(string col,string gObject){
		if(col == "blue"){
			wireC = GameObject.Find(gObject);
			Renderer rend = wireC.GetComponent<Renderer>();
			rend.material.SetColor("_Color", Color.blue);
		} else {
			wireC = GameObject.Find(gObject);
			Renderer rend = wireC.GetComponent<Renderer>();
			rend.material.SetColor("_Color", Color.red);
		}
		
			
	}

	public static void Shuffle<T>(Random random, T[,] array){	
    int lengthRow = array.GetLength(1);

    for (int i = 0; array.Length - 1 > i; i++){
        int i0 = i / lengthRow;
        int i1 = i % lengthRow;

        int j = Random.Range(i , array.Length);
        int j0 = j / lengthRow;
        int j1 = j % lengthRow;

        T temp = array[i0, i1];
        array[i0, i1] = array[j0, j1];
        array[j0, j1] = temp;
    	}
	}

	public void puzzle1Initializer (string padVer){
		Renderer p1Rend = new Renderer();
		Debug.Log(padVer);
		if(padVer == "puzzle1InputVer1"){
			Debug.Log("Versjon 1");
			Shuffle(rnd, puzzle1InputVer1);
			for(int i=0;i < puzzle1InputVer1.GetLength(0);i++){
				for(int j=0;j < puzzle1InputVer1.GetLength(1);j++){
					numKeys = GameObject.Find(puzzle1Nav[i,j]);
					p1Rend = numKeys.GetComponent<Renderer>();
					p1Rend.material.mainTexture = Resources.Load("numSymbol" + puzzle1InputVer1[i,j]) as Texture;
					
				}
			}
			puzzle1Input = puzzle1InputVer1;			
		} else {
			//Do generator for other version
			Debug.Log("Versjon 2");
			Shuffle(rnd, puzzle1InputVer2);
			for(int i=0;i < puzzle1InputVer2.GetLength(0);i++){
				for(int j=0;j < puzzle1InputVer2.GetLength(1);j++){
					numKeys = GameObject.Find(puzzle1Nav[i,j]);
					p1Rend = numKeys.GetComponent<Renderer>();
					p1Rend.material.mainTexture = Resources.Load("numSymbol" + puzzle1InputVer2[i,j]) as Texture;
					
				}
			}
			puzzle1Input = puzzle1InputVer2;
		}
	}
	public void puzzle3Initializer (){
		string colorSetOutput = "";

		for(int i = 0; i < p3LightColors.Length; i++){
			
			Color tmp1 = p3LightColors[i];
			string tmp2 = p3LightColorSet[i];
			int j = Random.Range(i, p3LightColors.Length);
			p3LightColors[i] = p3LightColors[j];
			p3LightColorSet[i] = p3LightColorSet[j];
			p3LightColors[j] = tmp1;
			p3LightColorSet[j] = tmp2;
			
		}
		Debug.Log(p3LightColorSet);
		for(int i = 0; i < p3LightColors.Length; i++){
			challengeComplete = GameObject.Find(p3Lights[i]);
			Renderer rend = challengeComplete.GetComponent<Renderer>();
			rend.material.SetColor("_Color", p3LightColors[i]);
		}

		for(int i = 0; i < p3LightColorSet.Length; i++){
			colorSetOutput = colorSetOutput + p3LightColorSet[i];
		}
		Debug.Log(colorSetOutput);
		
		puzzle3Solution = puzzle3Solutions[puzzle3SolIndex.IndexOf(colorSetOutput)];
		Debug.Log("p3 solution = " + puzzle3Solution);
	}

	public void puzzle4Screen (){
		puzzle4Rand = Random.Range(0,3);
		puzzle4TmpVal = puzzle4ScreenVal[Random.Range(0,puzzle4ScreenVal.Length)];
		//Debug.Log(puzzle4ScreenVal[puzzle4ScreenVal.Length-1]);
		pScreenP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("p4" + puzzle4TmpVal);
		pScreenP4.GetComponent<SpriteRenderer>().color = puzzle4Colors[puzzle4Rand];
		
	}
}
						
						
