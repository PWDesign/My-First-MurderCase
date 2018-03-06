using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinuteCountingPuzzleManager : Puzzle {

	public int correctAnswer;

	public Text inputText;
	public string playersAnswer = "";
	public int maxAnswerLength = 3;
	string spaces = "    ";

	protected override void Awake(){
		base.Awake ();
	}


	// Slide a number into the players answer
	public void InputNumber (int number){
		playersAnswer += number.ToString();
		string input = "";
		int index = 0;
		while (index <= playersAnswer.Length){
			input += playersAnswer [index];
			if (index == playersAnswer.Length -1){
				break;
			}
			input += spaces;
			index++;
		}
		inputText.text = input;

		// Reset if too many numbers entered
		if (playersAnswer.Length > maxAnswerLength){
			TooManyNumbers ();
		}
	}

	// Check if answer is correct
	public void EnterAnswer(){
		if (playersAnswer.Length > 0) {
			if (int.Parse (playersAnswer) == correctAnswer) {
				PuzzleCompleted ();
				return;
			}
			if (int.Parse (playersAnswer) > correctAnswer) {
				GameManager.instance.CallBlock ("RansomFail");
			} else {
				GameManager.instance.CallBlock("RansomFail");
            }
			ClearAnswer ();
		} else {
			GameManager.instance.CallBlock("RansomFail");
        }
	}


	protected override void PuzzleCompleted(){
		base.PuzzleCompleted ();
	}

	// clear the answer and tell the game manager to send a dialoague message to the player
	void TooManyNumbers(){
		GameManager.instance.PuzzleErrorMessage ("That's too many numbers!");
		ClearAnswer ();
	}

	// set the input to empty strings
	void ClearAnswer(){
		playersAnswer = "";
		inputText.text = "";
	}
}
