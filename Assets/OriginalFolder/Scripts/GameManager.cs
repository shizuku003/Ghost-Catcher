using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームプレイシーン管理用スクリプト
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapCheck mapCheck;
    private float gameStartTimer = 3f;// ゲーム開始までの時間
    private bool gameStartFlag;// ゲームスタート用フラグ
    [SerializeField]
    private float timer = 60f;// 初期設定ゲーム時間
    [SerializeField]
    private float chargeTime = 5f;// ゲーム時間回復値
    private bool timeUpFlag = false;// タイムアップ用フラグ
    private Text timerText;
    private Text scoreText;
    private Text resultText;

    private int point = 0;// 獲得ポイント

    private ScoreData scoreData;

    [SerializeField]
    private string resultSceneName = "ResultScene";// リザルトシーン名

    private GameSetting gameSetting;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        resultText = GameObject.Find("Result").GetComponent<Text>();

        scoreData = GameObject.Find("ScoreData").GetComponent<ScoreData>();

        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        timer = gameSetting.SendGameTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStartTimer > 0f)
        {
            gameStartTimer -= Time.deltaTime;
            timerText.text = "Ready...";
        }
        else
        {
            if (!gameStartFlag)
            {
                mapCheck.PreMapCheck();
                gameStartFlag = true;
            }
            Timer();
        }
    }

    /// <summary>
    /// タイマー用メソッド
    /// </summary>
    private void Timer()
    {
        if (timer < 0f)
        {
            timerText.text = "Time Up";
            if (!timeUpFlag)
            {
                resultText.text = "Result: " + point.ToString() + "pts";
                scoreData.ScoreReceive(point);
            }
            timeUpFlag = true;
            resultText.enabled = true;
            SceneManager.LoadScene(resultSceneName);
            return;
        }
        timer -= Time.deltaTime;
        timerText.text = "Timer: " + timer.ToString("F2") + "s";
    }

    /// <summary>
    /// ポイント獲得メソッド
    /// </summary>
    public void GetPoint()
    {
        if (!timeUpFlag)
        {
            point++;
            timer += chargeTime;
            scoreText.text = "Score: " + point.ToString() + "pts";
        }
    }
}
