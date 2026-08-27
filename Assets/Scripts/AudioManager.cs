using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // AudioManagerをシングルトンクラスにする
    // static -> クラス内で共通の値を持つ変数
    // AudioManagerクラスがシーン上にあるか検知するための変数

    static public AudioManager instance;

    void Awake()
    {
        //もしも変数instanceの中身がnill
        if(instance == null)
        {
            // 自身を変数instanceに登録
            instance = this;
        }else
        {
            //既に別のAudioManagerがいるので自身を破棄
            Destroy(this.gameObject);
        }

        //チャットGPTの変更点void startからawakeに
        seaudioSource = this.gameObject.AddComponent<AudioSource>();
        BGMaudioSource = this.gameObject.AddComponent<AudioSource>();
        BGMaudioSource.loop = false;
       
    }


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
    void Start()
    {
        // BGMを再生
        PlayBGM();
    }
}
