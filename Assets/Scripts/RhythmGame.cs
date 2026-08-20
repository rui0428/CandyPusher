using UnityEngine;

public class RhythmGame : MonoBehaviour
{
    // ==============================
    // AudioManager
    // ==============================

    // 既にあるAudioManagerを使用する
    public AudioManager audioManager;


    // ==============================
    // 拍
    // ==============================

    // 拍のPrefab
    public GameObject beatPrefab;

    // 拍が出現する場所
    public Transform spawnPoint;

    // 拍を叩く場所
    public Transform judgementPoint;


    // ==============================
    // 拍の移動
    // ==============================

    // 拍が出現してから判定位置まで移動する時間
    public float moveTime = 1.0f;


    // ==============================
    // 判定
    // ==============================

    // PERFECTの判定時間
    public float perfectTime = 0.05f;

    // GREATの判定時間
    public float greatTime = 0.10f;

    // GOODの判定時間
    public float goodTime = 0.18f;


    // ==============================
    // 拍を出すタイマー
    // ==============================

    float beatTimer = 0.0f;


    // ==============================
    // ゲーム開始
    // ==============================

    void Start()
    {
        // AudioManagerが設定されているか確認
        if (audioManager == null)
        {
            Debug.LogError("AudioManagerが設定されていません。");
            return;
        }

        // BGMを再生
        audioManager.PlayBGM();
    }


    // ==============================
    // 毎フレーム処理
    // ==============================

    void Update()
    {
        // 拍を出す
        CreateBeatTimer();

        // スペースキーが押されたら判定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JudgeBeat();
        }
    }


    // ==============================
    // 拍を出すタイマー
    // ==============================

    void CreateBeatTimer()
    {
        // 時間を進める
        beatTimer += Time.deltaTime;


        // 1秒経過したら拍を出す
        if (beatTimer >= 1.0f)
        {
            CreateBeat();

            // タイマーを0に戻す
            beatTimer = 0.0f;
        }
    }


    // ==============================
    // 拍を作る
    // ==============================

    void CreateBeat()
    {
        // 拍を作る
        GameObject beatObject = Instantiate(
            beatPrefab,
            spawnPoint.position,
            Quaternion.identity
        );


        // Beatスクリプトを取得
        Beat beat = beatObject.GetComponent<Beat>();


        // 拍の移動を設定
        beat.Setup(
            spawnPoint.position,
            judgementPoint.position
        );
    }


    // ==============================
    // タイミング判定
    // ==============================

    void JudgeBeat()
    {
        // BGMが何秒進んでいるか取得
        float currentTime =
            audioManager.BGMaudioSource.time;


        // 一番近い拍の時間を取得
        float nearestBeat =
            Mathf.Round(currentTime);


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