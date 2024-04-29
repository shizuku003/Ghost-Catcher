using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// シーン遷移用スクリプト
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private MapVisualCanceller mapVisualizer;
    [SerializeField]
    private string NextSceneName;// 遷移先シーン名

    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnControllerButtonDown += OnButtonDown;
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
    }

    /// <summary>
    /// ボタン押下処理メソッド
    /// </summary>
    void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.Bumper)// バンパーボタンを押下した場合
        {
            if (SceneManager.GetActiveScene().name == "MappingScene")
            {
                mapVisualizer.enabled = true;
            }
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
