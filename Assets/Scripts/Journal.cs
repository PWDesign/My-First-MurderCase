using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour {
	// This script needs to be optimized later
	GameObject ui;
	Animator animator;
	public GameObject firstPage;
	int currentPage;
	public int journalEntries;
	int totalEvidence = 17;
	int minEntries = 11;
	public int entriesPerPage = 5;
	public JournalPage[] pages;
	int currentEntryPage = 0;
    public Animator newEntry;

	public void AddEntry (string entryMessage){
		journalEntries++;
		if (pages[currentEntryPage].entries >= entriesPerPage){
			currentEntryPage++;		
		}
		pages [currentEntryPage].AddEntry (entryMessage);
		// Play an effect when a new entry is found
		newEntry.Play("NewEntry");

		// Check if there is enough evidence
		if (journalEntries >= minEntries){
			GameManager.instance.CallBlock("Time To Accuse");
		}
		if (journalEntries == totalEvidence){
			GameManager.instance.CallBlock("Must Accuse");
		}
	}
		
	void Awake(){
		ui = transform.GetChild (0).gameObject;
		animator = GetComponent<Animator> ();
	}

	// Hide the button to open the journal
	public void Hide(){
		ui.SetActive (false);	
	}
	// Show the journal button
	public void Show(){
		ui.SetActive (true);
	}

	// Open the journal and hie other UI
	public void Open(){
		animator.Play ("Open");
		GameManager.instance.journalOpen = true;
		GameManager.instance.BlockRaycasts ();
		GameManager.instance.DisableInGameMenuButton ();
	}
		
	public void FlipToPageTwo(){
		animator.Play("FlipToSecondPage");
	}
	public void FlipToPageOne(){
		animator.Play("FlipToFirstPage");
	}

	// Close the journal and return to game
	public void Close(){
		if (currentPage == 2){
			CloseFromSecondPage ();
			return;
		}
		animator.Play ("Close");
		GameManager.instance.journalOpen = false;
		GameManager.instance.UnblockRaycasts ();
		GameManager.instance.ResumeGame ();
	}

	public void CloseFromSecondPage(){
		animator.Play("CloseFromPageTwo");
		GameManager.instance.journalOpen = false;
		GameManager.instance.UnblockRaycasts ();
		GameManager.instance.ResumeGame ();
	}


	public void SetPageIndex1(){
		firstPage.transform.SetSiblingIndex (1);

	}
	public void SetPageIndex2(){
		firstPage.transform.SetSiblingIndex (2);
	}

	public void CurrentPage2(){
		currentPage = 2;
	}
	public void CurrentPage1(){
		currentPage = 1;
	}
}
