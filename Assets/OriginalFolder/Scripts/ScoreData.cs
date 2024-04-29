using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアデータ保持＆送信用スクリプト（シングルトン）
/// </summary>
public class ScoreData : MonoBehaviour
{
    public static ScoreData instance;

    private List<int> scores = new List<int>();// ゲームスコア保持用
    private int nowScore;// 現在のゲームスコア

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
    /// スコア受信メソッド
    /// </summary>
    /// <param name="score">ゲームスコア</param>
    public void ScoreReceive(int score)
    {
        scores.Add(score);
        nowScore = score;
    }

    /// <summary>
    /// スコア送信メソッド
    /// </summary>
    /// <returns>現在のゲームスコア</returns>
    public int ScoreSend()
    {
        return nowScore;
    }
}
