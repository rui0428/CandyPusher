using UnityEngine;
using UnityEngine.UI;

public class StageOut : MonoBehaviour
{
    //�v����`�FStageOut�N���X�̒��ɕϐ�Score���쐬�A�I�u�W�F�N�g�����蔲������Score��1���Z����
    //�P�DStageOut�N���X�̒��ɕϐ�Score���쐬
    //�Q�D�ϐ�Score��int�^����private�ł��邱��
    //�R�D�I�u�W�F�N�g�����蔲������(OnTriggerEnter���Ă΂ꂽ��)�ϐ�Score��1�����Z����
    //�S�D���Z��̕ϐ�Score��Debug.Log�ŃR���\�[����ɏo�͂���
    private int Score;
    public Text scoreText;
    // TextMeshProをコード上から取り扱う場合は、TextMeshProUGUI型が必要
    public TMPro.TextMeshProUGUI scoreTextTMP;

    public AudioManager audioManager;

    void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySE();
        //�ϐ���other�Ƃ́H
        //A,���蔲��������̃R���C�_�[���
        Debug.Log($"{other.name}がすり抜けました");
        //Destroy�֐�
        //Destry(�j�󂵂����I�u�W�F�N�g)
        //�I�u�W�F�N�g���g�p���Ă��郁�����̊J��(�K�x�[�W�R���N�V����)�ƕ`����̔j��
        Destroy(other.gameObject);
        Score = Score + 1;
        Debug.Log($"スコアが{Score}に増加しました");
        scoreTextTMP.text = $"Score:{Score}";

        if (Score >= 10)
        {
            // != (右辺と左辺の値が同じで無かったら)
            if(audioManager.BGMaudioSource.clip != audioManager.BGMaudioClips[1])
            {
            audioManager.BGMaudioSource.clip = audioManager.BGMaudioClips[1];
            audioManager.BGMaudioSource.Play();
            Debug.Log("BGMが変更されました");
            }
        }
    }
}
