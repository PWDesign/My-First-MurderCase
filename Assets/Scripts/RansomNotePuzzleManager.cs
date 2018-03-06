using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RansomNotePuzzleManager : Puzzle {

	List<WordHolder> blanks;
	List<DragDropWord> words;
	public Transform wordParent;
	public Transform blankParent;

	protected override void Awake(){
		base.Awake ();

		blanks = new List<WordHolder>();
		words = new List<DragDropWord>();	

		for (int i = 0; i < blankParent.childCount; i++){
			blanks.Add(blankParent.GetChild(i).GetComponent<WordHolder>());
		}
		for (int i = 0; i < wordParent.childCount; i++){
			words.Add(wordParent.GetChild(i).GetComponent<DragDropWord>());
		}
	}

	public override void CheckPuzzleCompletion(){
		for (int i = 0; i < blanks.Count; i++){
			if (blanks[i].correctWordInput != true){
				return;
			} 
		}
		// if it gets here this puzzle is completed
		PuzzleCompleted ();
	}

	protected override void ResetPuzzle(){
		foreach (WordHolder blank in blanks){
			blank.Reset ();
		}
		foreach (DragDropWord word in words){
			word.Reset ();
		}
	}

	protected override void PuzzleCompleted(){
		base.PuzzleCompleted ();
	}

}
