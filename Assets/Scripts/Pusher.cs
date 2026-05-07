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
        
    }
}
