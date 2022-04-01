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
    public Slider musicSlider;
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
        musicSlider.value = musicVolume;
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
            musicSlider.value = musicVolume;
            musicButton.GetComponent<Image>().sprite = musicOff;
            audioSource.Pause();
        } else {
            musicVolume = 1f;
            musicSlider.value = musicVolume;
            musicButton.GetComponent<Image>().sprite = musicOn;
            audioSource.Play();
        }
        save.musicVolume = musicVolume;
    }

    public void SetVolumeMusicBySlider (float vol) {
        musicVolume = vol;
        save.musicVolume = musicVolume;
        if(vol <= 0f) {
            musicButton.GetComponent<Image>().sprite = musicOff;
            audioSource.Pause();
        } else {
            musicButton.GetComponent<Image>().sprite = musicOn;
            audioSource.Play();
        }
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
