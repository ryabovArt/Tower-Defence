using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWaveTitle : MonoBehaviour
{
    [SerializeField] private GameObject waveTitle;
    [SerializeField] private Animator animator;

    public void ShowTitle()
    {
        waveTitle.SetActive(true);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("HideWaveTitle");
        yield return new WaitForSeconds(1f);
        waveTitle.SetActive(false);
    }
}
