using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour
{
    private AudioSource audioSource;

    public Sprite musicOn;
    public Sprite musicOff;
    public GameObject musicButton;

    public float musicVolume;

    private AudioClip[] music;

    private int currentClip;
    private float currentClipLength;

    private Save save;
    private void Start() {
        save = Save.instance;
        audioSource = GetComponent<AudioSource>();
        musicVolume = save.musicVolume;
        if (musicVolume > 0f) {
            musicButton.GetComponent<Image>().sprite = musicOn;
        } else {
            musicButton.GetComponent<Image>().sprite = musicOff;
        }

        music = Resources.LoadAll<AudioClip>("music") as AudioClip[];

        currentClip = Random.Range(0 , music.Length);
        audioSource.clip = music[currentClip];
        audioSource.Play();
    }

    private void FixedUpdate() {
        audioSource.volume = musicVolume;
        CheckAudioClipLength();
    }
    public void SetVolumeMusic () {
        if (musicVolume > 0f) {
            musicVolume = 0f;
            musicButton.GetComponent<Image>().sprite = musicOff;
            audioSource.Pause();
        } else {
            musicVolume = 1f;
            musicButton.GetComponent<Image>().sprite = musicOn;
            audioSource.Play();
        }
        save.musicVolume = musicVolume;
    }

    private void CheckAudioClipLength () {
        if(audioSource.isPlaying) {
            currentClipLength = currentClipLength + Time.deltaTime;
            if (currentClipLength >= audioSource.clip.length) {
            currentClip = Random.Range(0 , music.Length);
            audioSource.clip = music[currentClip];
            audioSource.Play();
            currentClipLength = audioSource.clip.length;
            currentClipLength = 0f;
            }
        }
    }
}
