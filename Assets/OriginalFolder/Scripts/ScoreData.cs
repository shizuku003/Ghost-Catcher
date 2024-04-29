using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�R�A�f�[�^�ێ������M�p�X�N���v�g�i�V���O���g���j
/// </summary>
public class ScoreData : MonoBehaviour
{
    public static ScoreData instance;

    private List<int> scores = new List<int>();// �Q�[���X�R�A�ێ��p
    private int nowScore;// ���݂̃Q�[���X�R�A

    void Awake()
    {
        CheckInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �X�R�A��M���\�b�h
    /// </summary>
    /// <param name="score">�Q�[���X�R�A</param>
    public void ScoreReceive(int score)
    {
        scores.Add(score);
        nowScore = score;
    }

    /// <summary>
    /// �X�R�A���M���\�b�h
    /// </summary>
    /// <returns>���݂̃Q�[���X�R�A</returns>
    public int ScoreSend()
    {
        return nowScore;
    }
}
