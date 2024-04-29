using UnityEngine;

/// <summary>
/// ゴースト生成用スクリプト
/// </summary>
public class GhostGenerator : MonoBehaviour
{
    [SerializeField]
    private int ghostMaxNum = 1;// ゲームシーンで生成するゴースト数の最大値
    [SerializeField]
    private GameObject[] ghost;// 生成するゴーストのプレファブ
    [SerializeField]
    private string ghostTag = "Ghost";// ゴーストオブジェクトが持つタグ名
    private bool firstGenerateFlag = false;// ゴーストの初期生成フラグ
    private int rnd;// ゴースト生成用乱数

    private float timer = 0f;// ゴースト生成用タイマー
    private float interval = 3f;// ゴースト生成までのインターバル
    private int ghostNum = 0;// 現在のゴースト数

    private GameSetting gameSetting;

    // Start is called before the first frame update
    void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        ghostMaxNum = gameSetting.SendGhostNum();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstGenerateFlag)// ゴーストの初期生成以降
        {
            timer += Time.deltaTime;
            if (timer > interval)// インターバルごとの処理
            {
                ghostNum = GameObject.FindGameObjectsWithTag(ghostTag).Length;
                if (ghostNum < ghostMaxNum)// 現在のゴースト数が設定した最大値から低下した場合
                {
                    for (int i = 0; i < (ghostMaxNum - ghostNum); i++)
                    {
                        GenerateGhost();
                    }
                }
            }
        }
    }

    /// <summary>
    /// ゴースト生成メソッド
    /// </summary>
    public void GenerateGhost()
    {
        rnd = Random.Range(0, ghost.Length - 1);
        Instantiate(ghost[rnd]);
    }

    /// <summary>
    /// ゴースト初期生成メソッド
    /// </summary>
    public void InstantiateGhost()
    {
        for (int i = 0; i < ghostMaxNum; i++)
        {
            GenerateGhost();
        }
        firstGenerateFlag = true;
    }
}
