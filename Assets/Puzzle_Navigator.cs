using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Navigator : MonoBehaviour {


public GameObject pointerNav;

public GameObject activeObj;
public GameObject numKeys;
public GameObject pBarP4;
public GameObject challengeComplete;

public GameObject wireC;

public Random rnd = new Random();
int selectionX, puzzleSelX1, puzzleSelX2, puzzleSelX3, puzzleSelX4;
int selectionY, puzzleSelY1, puzzleSelY2, puzzleSelY3, puzzleSelY4;
string[,] mainPuzzles = new string[,]{
	{("Box021"),("Box020")},
	{("Box019"),("Box018")},
};

int[] yearOptions = new int[]{1954, 1784, 1892};

//Numberpad display
GameObject numberDisplay1, numberDisplay2, yearDisplay;
Text displayNumber1, displayNumber2 , displayYear;

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
	1,1,1,1
};

bool leverAnimation = false;

//Puzzle4 Nav, Input & solution
string[,] puzzle4Nav = new string[,]{
	{("Box038"),("Box040")},
	{("Box039"),("Box041")},
};

string[,] puzzle4Input = new string [,]{
	{("A"),("C")},
	{("B"),("D")},
};

string puzzle4Progress;
string puzzle4Solution;

//challengeseed
int p1Ver;
string p2Ver;
//puzzlelocks
bool subPuzzleLock;
bool puzzle1Lock, puzzle2Lock, puzzle3Lock, puzzle4Lock;

//puzzleSolved
bool puzzle1Solved, puzzle2Solved, puzzle3Solved, puzzle4Solved;

	void Start(){

		pointerNav = GameObject.Find("Capsule");
		pBarP4 = GameObject.Find("ProgressSprite");
		
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
		//1954, 1784, 1892}
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

		int vertical = Input.GetKeyDown(KeyCode.S) ? 1: Input.GetKeyDown(KeyCode.W) ? -1:0;
		int horisontal = Input.GetKeyDown(KeyCode.D) ? 1: Input.GetKeyDown(KeyCode.A) ? -1:0;

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

			if(Input.GetKeyDown(KeyCode.Space) && puzzle1Resetting == false){
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
			if(Input.GetKeyDown(KeyCode.Space) && puzzle2Nav.IndexOf(puzzle2Nav[puzzleSelX2]) != 5){
				
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
			}else if(Input.GetKeyDown(KeyCode.Space)){

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
			if(Input.GetKeyDown(KeyCode.Space) && puzzle3Nav.IndexOf(puzzle3Nav[puzzleSelX3]) != 4){
				activeObj.transform.Rotate(Vector3.back, 40);
				if(slotValues[puzzleSelX3] < 9){
					slotValues[puzzleSelX3] += 1;
					Debug.Log(slotValues[puzzleSelX3] + ": value for slot " + puzzleSelX3);
				} else {
					Debug.Log("Back to one");
					slotValues[puzzleSelX3] = 1;
				}

			} else if (Input.GetKeyDown(KeyCode.Space)){
				if(activeObj.transform.rotation.eulerAngles.x == 300){
					//activeObj.transform.Rotate(Vector3.right, 120);
					StartCoroutine(leverAnimate());
					string p3Output = "";
					for(int i = 0;slotValues.Length > i; i++){
						p3Output = p3Output + slotValues[i].ToString();
					}
					if(p3Output == "1234"){
						Debug.Log("Challenge solved!");
						puzzle3Solved = true;
						subPuzzleLock = false;
						puzzle3Lock = false;
						activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
						pointerNav.transform.position = activeObj.transform.position;

						completionMarker("Challenge_solved3");
					} else {
						Debug.Log("Wrong input.");
					}
				}/* else {
					activeObj.transform.Rotate(Vector3.right, -120);
		
				}*/
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
				
				puzzle4Progress += puzzle4Input[puzzleSelX4,puzzleSelY4];
				
				if (puzzle4Progress == "C"){
					pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar1");
				} else if (puzzle4Progress == "CD"){
					pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar2");
				} else if (puzzle4Progress == "CDD"){
					pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar3");
				} else if (puzzle4Progress == "CDDA"){
					pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar4");
				} else if (puzzle4Progress == "CDDAB"){
					pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar5");
					Debug.Log("Challenge Solved");
					puzzle4Solved = true;
					subPuzzleLock = false;
					puzzle4Lock = false;
					activeObj = GameObject.Find(mainPuzzles[selectionX,selectionY]);
					pointerNav.transform.position = activeObj.transform.position;
					
					completionMarker("Challenge_solved4");
					
				} else {
					pBarP4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PBar0");
					Debug.Log("Puzzle reset");
					puzzle4Progress = "";
				}
				
			}
		}


		if(Input.GetKeyDown(KeyCode.Space) && subPuzzleLock == false){
			
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
		if(Input.GetKeyDown(KeyCode.Escape) && subPuzzleLock == true){
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
}
						
						
