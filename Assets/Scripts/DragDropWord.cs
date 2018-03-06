using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropWord : MonoBehaviour, IDragHandler ,IBeginDragHandler, IEndDragHandler {
	
	Vector3 m_StartingPosition;
	Transform m_startingParent;
	Transform dragParent;
	float offsetX;
	float offsetY;

	RansomNotePuzzleManager myManager;
	public int m_WordID;
	bool completed;
	// prevents multiple words from being picked up
	public static GameObject wordBeingDragged;


	void Awake(){
		m_StartingPosition = transform.position;
		m_startingParent = transform.parent;
		dragParent = GameObject.Find ("DraggedWordHolder").transform;
		myManager = GetComponentInParent<RansomNotePuzzleManager> ();
	}

	// Pick up a word that isn't in the correct spot yet
	public void OnBeginDrag (PointerEventData eventData) {
		if (!completed) {
			wordBeingDragged = gameObject;
			Vector3 mousepos = (Input.mousePosition);
			offsetX = mousepos.x - transform.position.x;
			offsetY = mousepos.y - transform.position.y;
			transform.SetParent (dragParent);
		}
	}

	// Move word with mouse
	public void OnDrag (PointerEventData eventData) {
		if (!completed)
		transform.position = Input.mousePosition - new Vector3 (offsetX, offsetY, 0);
	}

	// Drop word
	public void OnEndDrag (PointerEventData eventData) {
		if (!completed) {
			wordBeingDragged = null;
			// Reset position if not correctly matched
			if (transform.parent == dragParent) {
				Reset ();
			}
		}
	}

	// When corectly matched, set variables and position to matched state and check if puzzle is completed
	public void Matched(Transform holder){
		transform.SetParent (holder);
		completed = true;
		transform.localPosition = Vector3.zero;
		myManager.CheckPuzzleCompletion ();

	}

	// Resets the word to its original position in the puzzle
	public void Reset(){
		completed = false;
		transform.SetParent(m_startingParent);

		transform.position = m_StartingPosition;
	}
}
