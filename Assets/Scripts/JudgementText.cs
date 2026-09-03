using UnityEngine;

public class JudgementText : MonoBehaviour
{
    // 判定文字を表示しておく時間
    // Inspectorから変更できる
    public float displayTime = 0.5f;


    void Start()
    {
        // 指定した時間が経過したら
        // この判定文字を削除する
        Destroy(gameObject, displayTime);
    }
}