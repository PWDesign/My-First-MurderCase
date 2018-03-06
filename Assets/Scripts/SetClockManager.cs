using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetClockManager : Puzzle {

	public int correctTime;
	public int currentTime;

	public int hoursOnClock = 12;
	public enum ClockType {Analog, Digital};
	public ClockType changingType;
	public Transform hourHand;
	public Text digitalText;

	void Start(){
		if (changingType == ClockType.Analog){
			SetHourHand (currentTime);
			SetTimeText (correctTime);
		} else if (changingType == ClockType.Digital){
			SetTimeText (currentTime);
			SetHourHand (correctTime);
		}
	}
	public void HourUp(){
		currentTime++;
		if (currentTime > hoursOnClock){
			currentTime = 1;
		}
		SetTime ();
	}
	public void HourDown(){
		currentTime--;
		if (currentTime < 1 ){
			currentTime = hoursOnClock;
		}
		SetTime ();
	}

	public void SetHourHand(int time){
		float angle = 360 / hoursOnClock;
		hourHand.rotation = Quaternion.Euler (0, 0, -time * angle);
	}
	public void SetTimeText(int time){
		digitalText.text = time + ":00";
	}

	void SetTime(){
		if (changingType == ClockType.Analog){
			SetHourHand (currentTime);
		} else if (changingType == ClockType.Digital){
			SetTimeText (currentTime);
		}
	}

	public void ConfirmTime(){
		if (currentTime == correctTime){
			PuzzleCompleted ();
		} else {
			GameManager.instance.CallBlock ("RansomFail");
		}
	}
	protected override void PuzzleCompleted(){
		base.PuzzleCompleted ();
		// swap this for some sort of fungus trigger

	}
}
