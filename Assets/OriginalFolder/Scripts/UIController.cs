using UnityEngine;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// ステージセレクトシーンにおけるUI管理用スクリプト
/// </summary>
public class UIController : MonoBehaviour
{
    private LineRenderer lineRenderer;// ボタン選択用ライン

    [SerializeField]
    private string stageSelectButtonTag = "StageSelectButton";// ステージセレクトボタン用タグ名
    [SerializeField]
    private string gameSettingButtonTag = "GameSettingButton";// ゲーム設定ボタン用タグ名
    [SerializeField]
    private string showObjectButtonTag = "ShowObjectButton";// オブジェクト表示ボタン用タグ名

    void Start()
    {
        MLInput.OnControllerButtonDown += OnButtonDown;

        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, transform.position);// ボタン選択用ライン始点設定
        lineRenderer.SetPosition(1, transform.forward * 100);// ボタン選択用ライン終点設定
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
    }

    /// <summary>
    /// ボタン押下時処理メソッド
    /// </summary>
    void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.Bumper)// バンパーボタン押下時
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == stageSelectButtonTag)
                {
                    hit.transform.gameObject.GetComponent<StageSelectButton>().OnClick();
                }
                if (hit.transform.gameObject.tag == gameSettingButtonTag)
                {
                    hit.transform.gameObject.GetComponent<GameSettingButton>().OnClick();
                }
                if (hit.transform.gameObject.tag == showObjectButtonTag)
                {
                    hit.transform.gameObject.GetComponent<ShowObjectOnClick>().OnClick();
                }
            }
        }
    }
}
