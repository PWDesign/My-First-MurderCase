using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool journalOpen;
	Journal journal;

	public static GameManager instance;
	public Puzzle activePuzzle;
	public Button quitPuzzleButton;
	public Button exitGameButton;

	bool escapeMenuActive;
	public GameObject escapeMenu;
	public Button inGameMenuButton;
	public GameObject overlayUI;

    public GameObject raycastBlocker;

    public Flowchart flowchart;



	void Awake(){
		instance = this;
        if (flowchart == null)
        {
            flowchart = FindObjectOfType<Flowchart>();
        }
		journal = FindObjectOfType<Journal> ();
		DisableUI ();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			ToggleEscapeMenu ();
		}
	}
		
	#region puzzles

	// Old method for writing incorrect puzzle solutions
	public void PuzzleErrorMessage(string message){
		print (message);
	}

	// Tell the flowchart if the puzzle in question is completed or not
	public void CheckPuzzleCompletion(Puzzle puzzle){
		flowchart.SetBooleanVariable("completedPuzzle", puzzle.puzzleCompleted);
	}	

	// Being an uncompleted puzzle
    public void StartPuzzle(Puzzle puzzle)
    {
        flowchart.SetBooleanVariable("completedPuzzle", puzzle.puzzleCompleted);

        if (puzzle.puzzleCompleted == false)
        {
            activePuzzle = puzzle;
            activePuzzle.StartPuzzle();
        }
    }

	// Escape out of a completed puzzle
	public void ExitPuzzle(){
		if (activePuzzle != null) {
			activePuzzle.QuitPuzzle ();
			activePuzzle = null;
		}
		ResumeGame ();
	}

	// Bring up dialogue box for quitting a puzzle before completion
	public void QuitPuzzle(){
		ToggleEscapeMenu ();
		flowchart.ExecuteBlock ("QuitPuzzle");
	}
	#endregion

	#region Flowchart

	// Consolidate these 3 into 1 method later
	// Adjust volume of music and check for specific dialogue related to entering an area
	// Diable UI during transition. Use flowchart to reenable at the correct time.
	public void EnterPatio(){
		flowchart.ExecuteBlock ("Patio");
		DisableUI();
		AudioControl.instance.EnterPatio ();
	}

	public void EnterInterior(){
		flowchart.ExecuteBlock ("Interior");
		DisableUI();
		AudioControl.instance.EnterRestaurant ();
	}
	public void EnterKitchen(){
		flowchart.ExecuteBlock ("Kitchen");
		DisableUI();
		AudioControl.instance.EnterKitchen ();
	}
	// Make flowchart execute a specific block and turn off the journal button while talking
	public void CallBlock(string blockName){
		flowchart.ExecuteBlock (blockName);
		BlockRaycasts ();
		journal.Hide ();
	}

	// Bring back UI that should be available normally after talking
	public void DoneTalking(){
		UnblockRaycasts ();
		EnableUI ();
		journal.Show ();
	}
	#endregion

	#region ui
		
	public void ToggleEscapeMenu (){
		// when the journal is open close it first
		if (journalOpen){
			journal.Close ();
			return;
		}
		// resume game if the escape menu is open
		if (escapeMenuActive == true){
			ResumeGame ();

			// bring up the regular or puzzle menu
		} else {
			escapeMenu.SetActive (true);
			escapeMenuActive = true;
			DisableInGameMenuButton ();
			// if currently in a puzzle do the puzzle menu
			if (activePuzzle != null){
				exitGameButton.gameObject.SetActive (false);
				quitPuzzleButton.gameObject.SetActive (true);
				// otherwise do the normal menu
			} else {
				exitGameButton.gameObject.SetActive (true);
				quitPuzzleButton.gameObject.SetActive (false);
			}
		}
	}

	// Turn off all UI
	public void DisableUI(){
		overlayUI.SetActive (false);
	}
	// Allow UI to be on again
	public void EnableUI(){
		overlayUI.SetActive (true);
	}

	// Turn off the menu button
	public void DisableInGameMenuButton(){
		inGameMenuButton.gameObject.SetActive (false);
	}
	// Show the escape menu button
	public void ShowButton(){
		inGameMenuButton.gameObject.SetActive (true);
	}

	// Bring up an invisible object that stops raycasts from progressing the game in unwanted situation
	public void BlockRaycasts()
	{
		raycastBlocker.SetActive(true);
	}
	// Turn off the raycast blocker
	public void UnblockRaycasts()
	{
		raycastBlocker.SetActive(false);
	}

	// Turns off Escape menu
	public void ResumeGame (){
		escapeMenuActive = false;
		escapeMenu.SetActive (false);
		ShowButton ();
	}

	// Close the game
	// Should go to main menu instead, but need to fix bug
	public void ExitGame(){
		Application.Quit ();
		//SceneManager.LoadScene("Main Menu");
	}
	#endregion
}
