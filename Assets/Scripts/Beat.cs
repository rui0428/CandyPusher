using UnityEngine;

public class Beat : MonoBehaviour
{
    // ========================================
    // 設定
    // ========================================

    // 拍が移動する時間
    public float moveTime = 1.0f;


    // ========================================
    // 位置
    // ========================================

    // 拍が出現する位置
    Vector3 startPosition;

    // 拍を叩く位置
    Vector3 judgementPosition;


    // ========================================
    // 拍の準備
    // ========================================

    public void Setup(
        Vector3 start,
        Vector3 judgement
    )
    {
        // 出現位置を保存
        startPosition = start;

        // 判定位置を保存
        judgementPosition = judgement;

        // 拍を出現位置に置く
        transform.position = startPosition;
    }


    // ========================================
    // 毎フレーム
    // ========================================

    void Update()
    {
        // 判定位置に向かって移動
        transform.position = Vector3.MoveTowards(
            transform.position,
            judgementPosition,
            Vector3.Distance(startPosition, judgementPosition)
            * Time.deltaTime / moveTime
        );
    }
}