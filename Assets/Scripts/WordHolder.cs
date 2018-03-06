using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordHolder : MonoBehaviour, IDropHandler {
	public bool correctWordInput;
	public int m_WordID;

	bool CheckMatch (DragDropWord word){
		if (m_WordID == word.m_WordID) {
			CorrectMatch ();
			return true;
		}
		IncorrectMatch ();
		return false;
	}

	void IncorrectMatch(){
		GameManager.instance.CallBlock ("RansomFail");
	}

	void CorrectMatch(){
		correctWordInput = true;
	}

	public void Reset(){
		correctWordInput = false;
	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData) {
		DragDropWord word = DragDropWord.wordBeingDragged.GetComponent<DragDropWord>();
		if (CheckMatch (word)== true){
			word.Matched (transform);
		}
	}

	#endregion
}
