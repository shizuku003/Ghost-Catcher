using UnityEngine;

/// <summary>
/// クリックによるオブジェクト表示用スクリプト
/// </summary>
public class ShowObjectOnClick : MonoBehaviour
{
    [SerializeField]
    private GameObject showObject;// 表示するオブジェクト

    private bool clickedFlag;// クリックフラグ

    /// <summary>
    /// クリック時処理
    /// </summary>
    public void OnClick()
    {
        if (!clickedFlag)
        {
            showObject.SetActive(true);
            clickedFlag = true;
        }
        else
        {
            showObject.SetActive(false);
            clickedFlag = false;
        }
    }
}
