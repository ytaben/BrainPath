using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaySoundOnClick : MonoBehaviour {

    private SoundManager soundManager;
    private AudioSource mainAudioSource;
    private Button button;
    // Use this for initialization
    void Start()
    {
        soundManager = SoundManager.Instance();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        mainAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    void OnClick()
    {
        mainAudioSource.PlayOneShot(soundManager.mouseOverSound);
    }
}
