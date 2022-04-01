using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundScript : MonoBehaviour
{
    private AudioSource audioSource;

    public Sprite soundOn;
    public Sprite soundOff;
    public GameObject soundButton;

    public float soundVolume;
    public Slider soundSlider;
    private Save save;
    private void Start() {
        save = Save.instance;
        audioSource = GetComponent<AudioSource>();
        soundVolume = save.soundVolume;
        if (soundVolume > 0f) {
            soundButton.GetComponent<Image>().sprite = soundOn;
        } else {
            soundButton.GetComponent<Image>().sprite = soundOff;
        }
        soundSlider.value = soundVolume;
    }

    private void Update() {
        audioSource.volume = soundVolume;
    }
    public void PlaySound() {
        audioSource.Play();
    }

    public void SetVolumeSound () {
        if (soundVolume > 0f) {
            soundVolume = 0f;
            soundSlider.value = soundVolume;
            soundButton.GetComponent<Image>().sprite = soundOff;
        } else {
            soundVolume = 1f;
            soundSlider.value = soundVolume;
            soundButton.GetComponent<Image>().sprite = soundOn;
        }
        save.soundVolume = soundVolume;
    }
    public void SetVolumeSoundBySlider (float vol) {
        soundVolume = vol;
        save.soundVolume = soundVolume;
        if(vol <= 0f) {
            soundButton.GetComponent<Image>().sprite = soundOff;
        } else {
            soundButton.GetComponent<Image>().sprite = soundOn;
        }
    }
}
