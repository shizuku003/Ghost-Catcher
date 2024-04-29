using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲーム設定用スクリプト（シングルトン）
/// </summary>
public class GameSetting : MonoBehaviour
{
    public static GameSetting instance;

    [SerializeField]
    private string SelectSceneName = "StageSelectScene";// セレクトシーン名

    private float gameTime = 30f;// 初期設定ゲーム時間
    private int ghostNum = 5;// 初期設定ゴースト数

    private Text gameTimeText;
    private Text ghostNumText;

    void Awake()
    {
        CheckInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == SelectSceneName)
        {
            gameTimeText = GameObject.Find("Timer").GetComponent<Text>();
            ghostNumText = GameObject.Find("GhostNum").GetComponent<Text>();

            gameTimeText.text = Mathf.FloorToInt(gameTime) + "s";
            ghostNumText.text = ghostNum.ToString();
        }
    }

    /// <summary>
    /// シングルトン処理メソッド
    /// </summary>
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
    /// ゲーム時間設定用メソッド
    /// </summary>
    /// <param name="setTime">設定ゲーム時間</param>
    public void SetGameTime(int setTime)
    {
        gameTime += setTime;
        if (gameTime <= 0)
        {
            gameTime = 1;
        }
        gameTimeText.text = Mathf.FloorToInt(gameTime) + "s";
    }

    /// <summary>
    /// ゴースト数設定用メソッド
    /// </summary>
    /// <param name="setGhostNum">設定ゴースト数</param>
    public void SetGhostNum(int setGhostNum)
    {
        ghostNum += setGhostNum;
        if(ghostNum <= 0)
        {
            ghostNum = 1;
        }
        ghostNumText.text = ghostNum.ToString();
    }

    /// <summary>
    /// 初期設定ゲーム時間送信用メソッド
    /// </summary>
    /// <returns>初期設定ゲーム時間</returns>
    public float SendGameTime()
    {
        return gameTime;
    }

    /// <summary>
    /// 設定ゴースト数送信用メソッド
    /// </summary>
    /// <returns>設定ゴースト数</returns>
    public int SendGhostNum()
    {
        return ghostNum;
    }
}
