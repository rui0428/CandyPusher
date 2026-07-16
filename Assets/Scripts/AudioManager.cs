using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource seaudioSource;
    public AudioClip[] BGMaudioClips;
    public AudioSource BGMaudioSource;

    public void PlaySE()
    {
        seaudioSource.clip = audioClips[1];
        seaudioSource.Play();
    }

    public void PlayBGM()
    {

        BGMaudioSource.clip = BGMaudioClips[0];
        BGMaudioSource.Play();
    }

    public void PlayBGM2()
    {

        BGMaudioSource.clip = BGMaudioClips[1];
        BGMaudioSource.Play();
    }

    void Start()
    {
        seaudioSource = this.gameObject.AddComponent<AudioSource>();
        BGMaudioSource = this.gameObject.AddComponent<AudioSource>();
        BGMaudioSource.loop = true;
        PlayBGM();
    }

}
