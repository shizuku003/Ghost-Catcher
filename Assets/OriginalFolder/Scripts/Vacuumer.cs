using UnityEngine;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// 吸引器用スクリプト
/// </summary>
public class Vacuumer : MonoBehaviour
{
    [SerializeField]
    MapCheck mapCheck;
    private MLInput.Controller controller;// Magic Leap 1コントローラー

    private GameObject caughtGhost;// 吸引したゴースト
    private float distance = 0f;// ゴーストと吸引器の距離
    private GameManager gameManager;

    [SerializeField]
    private GhostGenerator ghostGenerator;
    [SerializeField]
    private string ghostTag = "Ghost";// ゴーストオブジェクトのタグ名

    [SerializeField]
    private Battery battery;
    private bool batteryLevelFlag;// バッテリー残量フラグ

    [SerializeField]
    private Transform lineStartPos;// 吸引方向表示用ライン始点
    [SerializeField]
    private LineRenderer lineRenderer;// 吸引方向表示用ライン

    [SerializeField]
    private GameObject vacuumedParticle;// 吸引時パーティクルオブジェクト

    [SerializeField]
    private AudioSource audioSource;// 音源出力用
    [SerializeField]
    private AudioClip vacuumingSound;// 吸引中音源
    [SerializeField]
    private AudioClip vacuumedSound;// 吸引後音源
    private bool vacuumingFlag;// 吸引中フラグ

    [SerializeField]
    private GameObject trapPrefab;// 罠プレファブ
    [SerializeField]
    private Transform trapPos;// 罠オブジェクト位置
    private GameObject trap;// 罠オブジェクト
    private bool trapFlag;// 罠生成フラグ

    void Start()
    {
        MLInput.OnControllerButtonDown += OnButtonDown;
        controller = MLInput.GetController(MLInput.Hand.Left);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, lineStartPos.position);// 吸引方向表示用ライン始点設定
        lineRenderer.SetPosition(1, lineStartPos.forward * 100);// 吸引方向表示用ライン終点設定
        CheckTrigger();
    }

    /// <summary>
    /// ボタン押下時処理メソッド
    /// </summary>
    void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.HomeTap)// ホームボタン押下時（罠生成＆破壊）
        {
            if (trapFlag)
            {
                trapFlag = false;
                DestroyObject(trap);
            }
            else
            {
                trapFlag = true;
                trap = Instantiate(trapPrefab, trapPos);
                trap.transform.parent = trapPos;
                trap.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

        }
        if (button == MLInput.Controller.Button.Bumper)// バンパーボタン押下時（罠射出）
        {
            if (trapFlag)
            {
                trap.transform.parent = null;
                trap.GetComponent<Rigidbody>().AddForce(trapPos.forward * 80f);
            }
        }
    }

    /// <summary>
    /// トリガー押下時処理メソッド（吸引処理）
    /// </summary>
    void CheckTrigger()
    {
        if (controller.TriggerValue > 0.3f)// トリガーを引いた時
        {
            Ray ray = new Ray(lineStartPos.position, lineStartPos.forward);
            RaycastHit hit;

            batteryLevelFlag = battery.BatteryUse();

            if (Physics.Raycast(ray, out hit) && batteryLevelFlag)
            {
                vacuumedParticle.SetActive(true);
                if (!vacuumingFlag)
                {
                    audioSource.PlayOneShot(vacuumingSound);
                    vacuumingFlag = true;
                }

                if (hit.collider.gameObject.tag == ghostTag)// バキューム対象がゴーストの時
                {
                    caughtGhost = hit.collider.gameObject;
                    caughtGhost.GetComponent<Ghost>().Vacuumed(hit.collider.transform.position, lineStartPos.position);

                    distance = Vector3.Distance(lineStartPos.position, hit.collider.transform.position);
                    if (distance < 0.5f)// ゴーストと吸引器の距離が一定距離以下になったとき
                    {
                        gameManager.GetPoint();
                        caughtGhost.GetComponent<Ghost>().DestroyGhost();
                        audioSource.PlayOneShot(vacuumedSound);
                        battery.BatteryCharge();
                        caughtGhost = null;
                    }
                }
            }

            if (!batteryLevelFlag)
            {
                vacuumedParticle.SetActive(false);
                audioSource.Stop();
                vacuumingFlag = false;

                if (caughtGhost != null)
                {
                    caughtGhost.GetComponent<Ghost>().DestroyGhost();
                }
            }
        }
        else
        {
            battery.BatteryAutoCharge();
            vacuumedParticle.SetActive(false);
            audioSource.Stop();
            vacuumingFlag = false;

            if (distance >= 0.5f)
            {
                if (caughtGhost != null)
                {
                    caughtGhost.GetComponent<Ghost>().DestroyGhost();
                    caughtGhost = null;
                }
            }
        }
    }
}
