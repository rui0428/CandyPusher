using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmGame : MonoBehaviour
{
    // ========================================
    // AudioManager
    // ========================================

    [Header("AudioManager")]

    // HierarchyにあるAudioManager
    public AudioManager audioManager;


    // ========================================
    // 拍
    // ========================================

    [Header("拍")]

    // 拍のPrefab
    public GameObject beatPrefab;

    // 拍が出現する場所
    public Transform spawnPoint;

    // 拍を叩く場所
    public Transform judgementPoint;


    // ========================================
    // 拍の設定
    // ========================================

    [Header("拍の設定")]

    // 何秒ごとに拍を出すか
    public float beatInterval = 1.0f;

    // 拍が出現してから判定位置まで移動する時間
    public float moveTime = 1.0f;


    // ========================================
    // 判定
    // ========================================

    [Header("判定")]

    // PERFECTの判定範囲
    public float perfectTime = 0.05f;

    // GREATの判定範囲
    public float greatTime = 0.10f;

    // GOODの判定範囲
    public float goodTime = 0.18f;


    // ========================================
    // タイマー
    // ========================================

    // 拍を出すためのタイマー
    float beatTimer = 0.0f;


    // ========================================
    // Start
    // ========================================

    void Start()
    {
        // AudioManagerが設定されているか確認
        if (audioManager == null)
        {
            Debug.LogError("AudioManagerが設定されていません。");
            return;
        }

        // 最初の拍までの時間
        beatTimer = beatInterval;
    }


    // ========================================
    // Update
    // ========================================

    void Update()
    {
        // 拍を出す
        beatTimer -= Time.deltaTime;

        if (beatTimer <= 0)
        {
            CreateBeat();

            // タイマーをリセット
            beatTimer = beatInterval;
        }


        // エンターキーを押したら判定
        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            JudgeBeat();
        }
    }


    // ========================================
    // 拍を作る
    // ========================================

    void CreateBeat()
    {
        // 拍を作る
        GameObject beatObject = Instantiate(
     beatPrefab,
     spawnPoint.position,
     Quaternion.identity,
     spawnPoint.parent
     );


        // Beatスクリプトを取得
        Beat beat = beatObject.GetComponent<Beat>();


        // Beatスクリプトがなかった場合
        if (beat == null)
        {
            Debug.LogError(
                "BeatPrefabにBeat.csが付いていません。"
            );

            return;
        }


        // 拍の移動時間を設定
        beat.moveTime = moveTime;


        // 拍のスタート位置と判定位置を設定
        beat.Setup(
            spawnPoint.position,
            judgementPoint.position
        );
    }


    // ========================================
    // 判定
    // ========================================

    void JudgeBeat()
    {
        // BGMが何秒進んでいるか取得
        float currentTime =
            audioManager.BGMaudioSource.time;


        // 1秒ごとの拍の中で、
        // 一番近い拍の時間を探す
        float nearestBeat =
            Mathf.Round(currentTime / beatInterval)
            * beatInterval;


        // 拍との時間のズレ
        float difference =
            Mathf.Abs(currentTime - nearestBeat);


        // PERFECT
        if (difference <= perfectTime)
        {
            Debug.Log("PERFECT!");
        }

        // GREAT
        else if (difference <= greatTime)
        {
            Debug.Log("GREAT!");
        }

        // GOOD
        else if (difference <= goodTime)
        {
            Debug.Log("GOOD!");
        }

        // MISS
        else
        {
            Debug.Log("MISS!");
        }
    }
}