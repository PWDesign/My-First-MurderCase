using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSwap : MonoBehaviour {

	public static CursorSwap instance;

	public Texture2D TalkTexture;
	public Texture2D InspectTexture;
	public Texture2D EnterTexture;

	public Vector2 talkHotSpot;
	public Vector2 inspectHotSpot;
	public Vector2 enterHotSpot;

	public CursorMode cursorMode = CursorMode.Auto;

	void Awake(){
		None ();
		instance = this;
	}
	public void Talk(){
		Cursor.SetCursor (TalkTexture, talkHotSpot, cursorMode);
	}
	public void Inspect(){
		Cursor.SetCursor (InspectTexture, inspectHotSpot, cursorMode);
	}
	public void Enter(){
		Cursor.SetCursor (EnterTexture, enterHotSpot, cursorMode);
	}
	public void None(){
		Cursor.SetCursor (null, Vector3.zero, cursorMode);
	}
}
