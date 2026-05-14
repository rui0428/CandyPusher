using UnityEngine;

public class Pusher : MonoBehaviour
{
    //Public=アクセス修飾子 float=型 Speed=変数 1f=初期値 ;=文の終わり
    public float speed = 1f;
    public float movePower = 5f;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = this.transform.position;
        Debug.Log("ゲームが開始したよ");
    }

    // Update is called once per frame
    void Update()
    {
        //Z軸の往復移動
        float z = Mathf.Sin(Time.time * speed) * movePower;
        
        //自身のローカル座標の位置を最初の位置情報にｚ(Sin波の変動値)を加算して返す
        // this.transform.localPosition 
        //↑このコードがアタッチ(入っている)されているオブジェクトのローカル座標の情報
        this.transform.localPosition = startPosition + new Vector3(0, 0, z);

    }
}
