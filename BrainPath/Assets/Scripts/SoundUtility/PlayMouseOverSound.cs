using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(EventTrigger))]
public class PlayMouseOverSound : MonoBehaviour, IPointerEnterHandler {
    private SoundManager soundManager;
    private AudioSource mainAudioSource;
    private Button button;
    // Use this for initialization
    void Start()
    {
        soundManager = SoundManager.Instance();
        button = GetComponent<Button>();
        mainAudioSource = Camera.main.GetComponent<AudioSource>();


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainAudioSource.PlayOneShot(soundManager.mouseOverSound);
    }
}
