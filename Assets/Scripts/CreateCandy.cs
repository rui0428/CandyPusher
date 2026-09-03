using System.Threading;
using UnityEngine;
// InputSystemを使用するのでusing UnityEngine.InputSystemを追加
using UnityEngine.InputSystem;


public class CreateCandy : MonoBehaviour
{
    public float speed = 1f;
    public float movepower = 7f;
    public float Timer = 0f;
    private Vector3 startPosition;
    public Rigidbody InstantiatedCandy;

    // スペースキーが押されたら、CandyPrefabを生成する
    // 1,スペースが押された時の判定
    // 2,CandyPrefabを生成する

    // 生成したいオブジェクトを変数として定義
    [SerializeField]
    private GameObject candyPrefab;
    void Start()
    {
        startPosition = this.transform.position;
    }

    // スペースが押された時の判定
    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * movepower;
        InstantiatedCandy.linearVelocity = new Vector3(x, 0, 0);

        // もしも接続状態のキーボードのスペースキーが押されたら
        // デバイス                  ：keyboard => キーボードに関する処理を呼び出す
        // デバイスの状態            ：current => 現在接続状態のキーボードを取得する
        // デバイスの欲しいキーの情報：spaceKey => スペースキーの情報を取得する
        // キーの状態                ：wasPressedThisFrame => 押された瞬間かどうかの判定
        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
            // オブジェクトを生成する処理
            //Debug.Log("スペースキーが押された");

            // Instantiate => オブジェクトを実体化する関数
            // Instantiate(生成したいオブジェクト);
            // 変数InstantiateCandyを定義 初期値を生成したオブジェクトに設定
            //GameObject InstantiatedCandy = Instantiate(candyPrefab);

            // 生成したオブジェクトの位置をこのスクリプトがアタッチされているオブジェクトと同じに変更
            //InstantiatedCandy.transform.position = this.transform.position;            
        //}
        //Timer += Time.deltaTime;
        //if(Timer>=1f)
        //{
            //GameObject InstantiatedCandy = Instantiate(candyPrefab);
            //InstantiatedCandy.transform.position = this.transform.position;

            // Debug Logを使い、逐一状況を確認できる状態にする        
            //Debug.Log("秒数が経過");
            //Timer = 0;
        //}
    }
}
//一秒間に右側に1f動かしたい(右方向 → xの値を加算)
//代入先(vector3:(x,y,z))
//this.transform.position = this.transform.position + new vector3(moveSpeed,0,0) * Time.deltatime;
//もしも現在位置のxの値が5よりも大きかったら
//if(this.transform.position,x > 5)
//{
//  移動方向を反対にする
//  movSpeed = -1 * moveSpeed;
//}
//もしも現在位置のxの値が5よりも小さかったら
//if(this.transform.position.x < -5)
//{
//  移動方向を反対にする
//  moveSpeed = -1 * moveSpeed;
//}