using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalPage : MonoBehaviour {

	public int entries = 0;

	Text[] Entry;
	// 
	public void AddEntry(string logEntry){
		// Check if this page exists
		// If it doesn't create the text fields for it
		if (Entry == null){
			Entry = new Text[5];
			for (int i = 0; i < transform.childCount; i++){
				Entry [i] = transform.GetChild (i).GetComponent<Text> ();
			}
		}
		// add text to entry
		Entry [entries].text = logEntry;
		entries++;
	}

}
