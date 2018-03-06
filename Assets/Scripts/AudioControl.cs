using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {
	AudioReverbFilter arf;
	AudioSource asrc;
	public static AudioControl instance;

	void Awake(){
		instance = this;
		arf = GetComponent<AudioReverbFilter> ();
		arf.reverbPreset = AudioReverbPreset.Off;
		asrc = GetComponent<AudioSource> ();
	}

	// Set volumes and audio effects for scenes
	public void EnterPatio(){
		arf.enabled = true;
		arf.reverbPreset = AudioReverbPreset.Forest;
		asrc.volume = 1;
	}
	public void EnterRestaurant(){
		arf.enabled = false;
		asrc.volume = .4f;

	}
	public void EnterKitchen(){
		asrc.volume = 1;
		arf.enabled = true;
		arf.reverbPreset = AudioReverbPreset.Room;
	}

	// Start playing the song
	public void PlayMusic(){
		asrc.Play ();
	}
}
