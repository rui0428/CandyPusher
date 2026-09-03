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

    [Header("PERFECTボーナス")]
    public GameObject candyPrefab;
    public Transform candyDropPoint;
    public int perfectCandyCount = 10;

    [Header("判定オブジェクト")]
    public GameObject perfectObject;
    public GameObject greatObject;
    public GameObject goodObject;
    public GameObject missObject;

    // 判定文字を表示する場所
    public Transform judgementTextPoint;

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
        if (Keyboard.current.enterKey.wasPressedThisFrame)
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
        // 画面上に存在しているBeatをすべて取得する
        Beat[] beats = FindObjectsOfType<Beat>();


        // 判定できるBeatが存在しない場合は処理を終了する
        if (beats.Length == 0)
        {
            return;
        }


        // JudgementPointに一番近いBeatを保存する
        Beat targetBeat = null;


        // 一番近いBeatまでの距離を保存する
        float closestDistance = float.MaxValue;


        // 画面上にあるBeatを1つずつ確認する
        foreach (Beat beat in beats)
        {
            // BeatからJudgementPointまでの距離を計算する
            float distance =
                Vector3.Distance(
                    beat.transform.position,
                    judgementPoint.position
                );


            // 今まで調べたBeatよりも近かった場合
            if (distance < closestDistance)
            {
                // 一番近いBeatとして保存する
                closestDistance = distance;

                targetBeat = beat;
            }
        }


        // 判定するBeatが見つからなかった場合は処理を終了する
        if (targetBeat == null)
        {
            return;
        }


        // ========================================
        // Beatの移動距離を取得
        // ========================================

        // Beatが出現する位置から
        // JudgementPointまでの距離を取得する
        float totalDistance =
            Vector3.Distance(
                spawnPoint.position,
                judgementPoint.position
            );


        // ========================================
        // Beatの位置から判定時間を計算
        // ========================================

        // 現在のBeatがJudgementPointから
        // どれくらい離れているかを計算する
        float distanceFromJudgement =
            Vector3.Distance(
                targetBeat.transform.position,
                judgementPoint.position
            );


        // Beatが判定位置から離れている割合を計算する
        float distanceRate =
            distanceFromJudgement / totalDistance;


        // Beatが判定位置から離れている時間を計算する
        // moveTimeを使うことで、
        // Beatの移動速度に合わせた判定になる
        float timeDifference =
            distanceRate * moveTime;


        // ========================================
        // PERFECT
        // ========================================

        // 判定位置からの時間のズレが
        // PERFECTの範囲内ならPERFECT
        if (timeDifference <= perfectTime)
        {
            // PERFECTと表示する
            Debug.Log("PERFECT!");


            // PERFECT用のオブジェクトを生成する
            // JudgementPointの位置に生成する
            Instantiate(
                perfectObject,
                judgementTextPoint.position,
                Quaternion.identity,
                judgementTextPoint.parent
             );


            // PERFECTになったBeatを削除する
            Destroy(targetBeat.gameObject);


            // PERFECTなのでキャンディを指定した数だけ生成する
            for (int i = 0; i < perfectCandyCount; i++)
            {
                // CandyDropPointを基準にして
                // 生成位置を少しずつ左右にずらす
                float offsetX = (i - (perfectCandyCount - 1) / 2.0f) * 1f;

                // Candyを生成する位置を計算する
                Vector3 spawnPosition =
                    candyDropPoint.position + new Vector3(offsetX, 0, 0);

                // CandyPrefabを計算した位置に生成する
                Instantiate(
                    candyPrefab,
                    spawnPosition,
                    Quaternion.identity
                );
            }
        }


        // ========================================
        // GREAT
        // ========================================

        // PERFECTではないが、
        // GREATの範囲内ならGREAT
        else if (timeDifference <= greatTime)
        {
            Debug.Log("GREAT!");


            // GREATになったBeatを削除する
            Destroy(targetBeat.gameObject);
        }


        // ========================================
        // GOOD
        // ========================================

        // PERFECT・GREATではないが、
        // GOODの範囲内ならGOOD
        else if (timeDifference <= goodTime)
        {
            Debug.Log("GOOD!");


            // GOODになったBeatを削除する
            Destroy(targetBeat.gameObject);
        }


        // ========================================
        // MISS
        // ========================================

        // GOODの範囲よりも離れている場合
        else
        {
            Debug.Log("MISS!");


            // MISSの場合はBeatを削除しない
            // BeatがJudgementPointまで移動すると
            // Beat.cs側で自動的に削除される
        }
    }
}