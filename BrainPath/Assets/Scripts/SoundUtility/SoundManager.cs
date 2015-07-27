using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public AudioClip mouseOverSound;
    public AudioClip mouseClickSound;
    public AudioClip correctActionSound;
    public AudioClip wrongActionSound;

    private static SoundManager instance;

    public static SoundManager Instance()
    {
        if (instance) return instance;
        else return instance = new SoundManager();
    }

	// Use this for initialization
	void Awake () {
        instance = this;
	}
}
