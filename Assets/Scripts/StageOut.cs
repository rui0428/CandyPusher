using UnityEngine;
using UnityEngine.UI;

public class StageOut : MonoBehaviour
{
    private int Score;
    public Text scoreText;

    // TextMeshProをコード上から取り扱う場合は、TextMeshProUGUI型が必要
    public TMPro.TextMeshProUGUI scoreTextTMP;
    //リファクタリング:TextMeshProUGUI -> 画面上のテキストを高画質で表示するためのコンポーネント

    void OnTriggerEnter(Collider other)
    //リファクタリング:Collider other -> 触れた相手のコライダーの情報を受け取るための変数
    {
        AudioManager.instance.PlaySE();
        
        //Debug.Log($"{other.name}がすり抜けました");
       
        Destroy(other.gameObject);
        Score = Score + 1;
        //Debug.Log($"スコアが{Score}に増加しました");
        scoreTextTMP.text = $"Score:{Score}";

        if (Score >= 10)
        {
            // != (右辺と左辺の値が同じで無かったら)
            if(AudioManager.instance.BGMaudioSource.clip != AudioManager.instance.BGMaudioClips[1]) 
            {
                AudioManager.instance   .BGMaudioSource.clip = AudioManager.instance.BGMaudioClips[1];
                AudioManager.instance.BGMaudioSource.Play();
                //Debug.Log("BGMが変更されました");

            }
        }
    }
}
