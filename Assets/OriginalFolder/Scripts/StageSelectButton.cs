using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージセレクトボタン用スクリプト
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    [SerializeField]
    private string sceneName;// 遷移先シーン名

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
