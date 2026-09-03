using UnityEngine;

public class Beat : MonoBehaviour
{
    // BeatがSpawnPointからJudgementPointまで
    // 移動するのにかかる時間
    // 例：1.0なら約1秒で移動する
    public float moveTime = 1.0f;


    // このBeat自身のRectTransformを保存する
    // UIのImageを動かすために使用する
    RectTransform rectTransform;


    // Beatが最初に出現した位置
    Vector2 startPosition;

    // Beatが判定される位置
    Vector2 judgementPosition;


    void Awake()
    {
        // このBeatについているRectTransformを取得する
        // BeatがCanvasのImageなので、UIの位置を扱うために必要
        rectTransform = GetComponent<RectTransform>();
    }


    public void Setup(Vector3 start, Vector3 judgement)
    {
        // BeatをSpawnPointの位置に移動する
        // ここがBeatが最初に出現する場所
        rectTransform.position = start;


        // 最初の位置を保存する
        // あとで「どれくらい移動するか」を計算するために使う
        startPosition = rectTransform.position;


        // 判定位置を保存する
        // Beatは最終的にこの位置まで移動する
        judgementPosition = judgement;
    }


    void Update()
    {
        // BeatをJudgementPointに向かって移動させる
        //
        // transform.positionではなく
        // RectTransformのpositionを使っている
        // これはBeatがCanvas内のUIだから
        rectTransform.position = Vector3.MoveTowards(

            // 現在のBeatの位置
            rectTransform.position,

            // 移動先
            judgementPosition,

            // 1フレームで移動する距離
            // 「移動する距離 × 経過時間 ÷ 移動にかかる時間」
            // という計算になっている
            Vector3.Distance(startPosition, judgementPosition)
            * Time.deltaTime / moveTime
        );


        // BeatがJudgementPointに近づいたか確認する
        //
        // 0.1fより近くなったら
        // 「判定位置まで到着した」と判断する
        if (Vector3.Distance(
            rectTransform.position,
            judgementPosition
        ) < 0.1f)
        {
            // 判定位置まで到着したBeatを削除する
            //
            // gameObjectは、このBeat自身を意味する
            // Destroyすると画面からBeatが消える
            Destroy(gameObject);
        }
    }
}