using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MisspelledWordPuzzleManager : Puzzle {
	public Transform misspelledWordsParent;
	List<MisspelledWord> wordsToFix;
	[TextArea(11,11)]
	public string paragraph;
	public string[] separatedParagraph;
	public int charsInLine = 40;
	int newlineIndex = 0;

	public Text textPrefab;
	public GameObject rowPrefab;

	public int[] misspelledWordIndex;
	public string [] mispelling;
	public int[] newLineAtWordIndex;
	List<Text> words;
	int wordsLeftToFix;
	string failureBlock = "MisspellFailed";

	Vector3 startingLocation;

	protected override void Awake(){
		base.Awake ();
		// split up the text into words
		separatedParagraph = paragraph.Split (' ');
		wordsToFix = new List<MisspelledWord> ();
		words = new List<Text> ();
		int charCount = 0;
		int row = 0;
		// Split words into rows
		for (int i = 0; i < separatedParagraph.Length; i++){
			if (newLineAtWordIndex.Length > 0) {
				if (newlineIndex < newLineAtWordIndex.Length){
					if (newLineAtWordIndex [newlineIndex] == i) {
						newlineIndex++;
						row++;
						Instantiate (rowPrefab, misspelledWordsParent);
						row++;
						Instantiate (rowPrefab, misspelledWordsParent);
						charCount = 0;
					}

				}
			}
			charCount += separatedParagraph[i].Length;
			if (charCount > charsInLine){
				row++;
				charCount = separatedParagraph[i].Length;
			}
			if (misspelledWordsParent.childCount < row+1){
				Instantiate (rowPrefab, misspelledWordsParent);
			}
			Text newWord = (Text)Instantiate (textPrefab, misspelledWordsParent.GetChild(row));
			newWord.text = separatedParagraph [i];
			words.Add (newWord);
		}
		// Find the words that are misspelled and attach the component
		if (misspelledWordIndex.Length > 0) {
			for (int i = 0; i < misspelledWordIndex.Length; i++) {
				wordsToFix.Add (words [misspelledWordIndex [i]].gameObject.AddComponent<MisspelledWord> ());
				wordsToFix [i].SetSpelling (mispelling [i], words [misspelledWordIndex [i]].text);
			}
			wordsLeftToFix = wordsToFix.Count;
		}
	}

	// count down the amount of words left when one is corrected 
	public void WordCorrected(){
		wordsLeftToFix--;
	}

	// exit puzzle when all words are corrected
	public void CheckComplete(){
		if (wordsLeftToFix <= 0){
			PuzzleCompleted ();
			return;
		} else {
			GameManager.instance.CallBlock (failureBlock);
		}
	}

	protected override void PuzzleCompleted(){
		base.PuzzleCompleted ();

	}
}
