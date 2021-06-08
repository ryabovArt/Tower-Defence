using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private Slider slider;

    [SerializeField] private AudioSource backgroundMusic;
    private float deltaVolume = 0.1f;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
    }

    public void ChangeVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-40, 0, volume));
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void FadeMusic()
    {
        StartCoroutine(FadeMusicCorutine());
    }

    IEnumerator FadeMusicCorutine()
    {
        while (backgroundMusic.volume > 0)
        {
            backgroundMusic.volume -= deltaVolume;
            yield return new WaitForSeconds(0.1f);
            print("FadeMusicCorutine()");
        }
    }
}
