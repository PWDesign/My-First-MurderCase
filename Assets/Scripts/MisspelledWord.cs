using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MisspelledWord : MonoBehaviour, IPointerClickHandler {
	
	public bool corrected;
	string correctSpelling;
	Text m_Text;
	MisspelledWordPuzzleManager manager;

	void Awake(){
		manager = GetComponentInParent<MisspelledWordPuzzleManager> ();
		m_Text = GetComponent<Text> ();
	}

	// Set the spelling to the incorrect spelling, and store the correct spelling
	public void SetSpelling(string incorrectSpelling, string properSpelling){
		m_Text = GetComponent<Text> ();
		m_Text.text = incorrectSpelling;
		correctSpelling = properSpelling;
	}

	public void Reset(){
		corrected = false;
	}

	// Set word to the correct spelling if it hasn't been correct yet
	public void OnPointerClick (PointerEventData eventData) {
		if (corrected == false){
			CorrectSpelling ();
		}
	}

	// change string to the correct spelling and check if the puzzle is complete
	void CorrectSpelling(){
		m_Text.text = correctSpelling;
		corrected = true;
		manager.WordCorrected ();
	}
}
