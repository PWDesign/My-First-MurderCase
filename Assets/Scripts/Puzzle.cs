using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

	public bool puzzleCompleted = false;
	public string blockToCallWhenComplete;
	protected GameObject PuzzleUI;

	protected virtual void  Awake(){
		PuzzleUI = transform.GetChild (0).gameObject;
	}

	// might call this from the game manager instead depending on fungus functionlity
	// Start a puzzle if it isn't completed already
	public bool CheckAlreadyCompleted(){
		if (puzzleCompleted == false){
			StartPuzzle ();
			return false;
		}
		return true;
	}
		
	public void StartPuzzle(){
		PuzzleUI.SetActive (true);
		ResetPuzzle ();
	}

	public virtual void CheckPuzzleCompletion(){
		
	}

	protected virtual void ResetPuzzle(){
		
	}

	// set puzzle to completed, call fungus dialogue for complete puzzle, and exit puzzle 
	protected virtual  void PuzzleCompleted(){
		puzzleCompleted = true;
		// swap this for some sort of fungus trigger
		if (blockToCallWhenComplete != "") {
			GameManager.instance.CallBlock (blockToCallWhenComplete);
		} else {
			GameManager.instance.ExitPuzzle ();
		}
	}
		
	public void QuitPuzzle(){
		PuzzleUI.SetActive (false);
	}
}
