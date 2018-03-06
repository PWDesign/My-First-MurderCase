using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOptionActivityManager : Puzzle{

	// Set these in the buttons in the hierarchy/inspector

	public void RightAnswer(){
		PuzzleCompleted ();
	}
		
	public void WrongAnswer(){
		GameManager.instance.CallBlock ("RansomFail");
    }

	protected override void PuzzleCompleted(){
		base.PuzzleCompleted ();

	}
}
