using UnityEngine;

public class StageOut : MonoBehaviour
{
    //要件定義：StageOutクラスの中に変数Scoreを作成、オブジェクトがすり抜けたらScoreを1加算する
    //１．StageOutクラスの中に変数Scoreを作成
    //２．変数Scoreはint型かつprivateであること
    //３．オブジェクトがすり抜けたら(OnTriggerEnterが呼ばれたら)変数Scoreに1を加算する
    //４．加算後の変数ScoreをDebug.Logでコンソール上に出力する

    void OnTriggerEnter(Collider other)
    {
        //変数名otherとは？
        //A,すり抜けた相手のコライダー情報
        Debug.Log($"{other.name}がすり抜けました。");
        //Destroy関数
        //Destry(破壊したいオブジェクト)
        //オブジェクトが使用しているメモリの開放(ガベージコレクション)と描画情報の破棄
        Destroy(other.gameObject);
    }
}
