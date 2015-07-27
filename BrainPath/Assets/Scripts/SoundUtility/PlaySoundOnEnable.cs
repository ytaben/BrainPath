using UnityEngine;
using System.Collections;

public class PlaySoundOnEnable : MonoBehaviour {

    public AudioClip sound;
    AudioSource mainAudioSource;
	// Use this for initialization
	void Awake () {
        mainAudioSource = Camera.main.GetComponent<AudioSource>();
        if (!mainAudioSource) Debug.Log("Main camera doesn't have Audio Source!");
	}
	
	void OnEnable()
    {   if (!sound) Debug.Log("bad");
        mainAudioSource.PlayOneShot(sound);
    }
}
