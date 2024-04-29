using UnityEngine;

/// <summary>
/// ゴースト用スクリプト
/// </summary>
public class Ghost : MonoBehaviour
{
    private Transform playerPos;// プレイヤー位置
    private Vector3 startPos;// ゴースト移動始点
    private Vector3 endPos;// ゴースト移動終点
    [SerializeField]
    private float speed = 0.5f;// ゴースト移動速度
    private float time;// 移動処理用タイマー

    private MapCheck mapCheck;
    private float[] mapInfo = new float[6];// 簡易マップ情報
    private Vector3[] positions = new Vector3[6];// 簡易マップ情報（Vector3型）

    private float vacuumSpeed = 0.5f;// 吸引時移動速度
    private float vacuumTime;// 吸引時移動処理用タイマー
    private bool vacuumFlag = false;// 吸引フラグ

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Main Camera").transform;
        mapCheck = GameObject.Find("Main Camera").GetComponent<MapCheck>();
        PositionsInitialized();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerPos.position);// ゴーストが常にプレイヤー方向を向くように

        if (transform.position != endPos)// ゴーストが移動時の終点にたどり着いていない場合
        {
            if (vacuumFlag)// ゴーストが吸引中の処理
            {
                vacuumTime += Time.deltaTime;
                transform.position = Vector3.MoveTowards(startPos, endPos, vacuumTime * vacuumSpeed);
            }
            else
            {
                time += Time.deltaTime;
                transform.position = Vector3.MoveTowards(startPos, endPos, time * speed);
            }
        }
        else// ゴーストが終点にたどり着いた場合（各変数の初期化）
        {
            time = 0f;
            vacuumFlag = false;
            vacuumTime = 0f;
            PositionsInitialized();
        }
    }

    /// <summary>
    /// 移動用座標情報の初期化処理メソッド
    /// </summary>
    private void PositionsInitialized()
    {
        mapInfo = mapCheck.MapInfoSend();
        positions[0] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[0]), Random.Range(mapInfo[2] - 1.5f, mapInfo[3] + 1.5f), Random.Range(mapInfo[4] - 1.5f, mapInfo[5] + 1.5f));
        positions[1] = new Vector3(Random.Range(mapInfo[1], mapInfo[1] + 1.5f), Random.Range(mapInfo[2] - 1.5f, mapInfo[3] + 1.5f), Random.Range(mapInfo[4] - 1.5f, mapInfo[5] + 1.5f));
        positions[2] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[1] + 1.5f), Random.Range(mapInfo[2], mapInfo[2] - 1.5f), Random.Range(mapInfo[4] - 1.5f, mapInfo[5] + 1.5f));
        positions[3] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[1] + 1.5f), Random.Range(mapInfo[3], mapInfo[3] + 1.5f), Random.Range(mapInfo[4] - 1.5f, mapInfo[5] + 1.5f));
        positions[4] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[1] + 1.5f), Random.Range(mapInfo[2] - 1.5f, mapInfo[3] + 1.5f), Random.Range(mapInfo[4], mapInfo[4] - 1.5f));
        positions[5] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[1] + 1.5f), Random.Range(mapInfo[2] - 1.5f, mapInfo[3] + 1.5f), Random.Range(mapInfo[5], mapInfo[5] + 1.5f));

        CheckPos();
    }

    /// <summary>
    /// 移動用座標の選択用メソッド
    /// </summary>
    void CheckPos()
    {
        int rndS = Random.Range(0, 5);
        int rndE = Random.Range(0, 5);
        while (rndS == rndE)
        {
            rndE = Random.Range(0, 5);
        }

        startPos = positions[rndS];
        transform.position = startPos;
        endPos = positions[rndE];
    }

    /// <summary>
    /// 吸引処理用メソッド
    /// </summary>
    /// <param name="currentPoint">ゴーストの現在位置</param>
    /// <param name="weaponPoint">吸引器の現在位置</param>
    public void Vacuumed(Vector3 currentPoint, Vector3 weaponPoint)
    {
        vacuumFlag = true;
        startPos = currentPoint;
        endPos = weaponPoint;
    }

    /// <summary>
    /// ゴースト破壊用メソッド
    /// </summary>
    public void DestroyGhost()
    {
        DestroyObject(gameObject);
    }
}