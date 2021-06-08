using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [Header("Задержка перед проигрыванием музыки после гонга")]
    [SerializeField] private float delayAfterGong;

    public void StartWaves()
    {
        audioSources[0].Stop();
        audioSources[2].Play();
        StartCoroutine(PlayAfterStartWavesSound());
    }
    IEnumerator PlayAfterStartWavesSound()
    {
        yield return new WaitForSeconds(delayAfterGong);
        audioSources[1].Play();
    }
}
