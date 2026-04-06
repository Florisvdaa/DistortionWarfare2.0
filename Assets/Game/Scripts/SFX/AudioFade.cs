using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] private float maxVolume = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Debug.Log(audioSource);
    }

    public void StartFade()
    {
        StartCoroutine(Fade(audioSource,fadeTime));
    }

    public static IEnumerator Fade(AudioSource audio, float fadeTime)
    {
        float startVolume = 0.2f;

        audio.volume = 0;
        audio.Play();

        while (audio.volume < 0.5f)
        {
            audio.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audio.volume = .5f;
    }

}
