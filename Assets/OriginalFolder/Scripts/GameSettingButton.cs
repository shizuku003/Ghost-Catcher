using UnityEngine;

/// <summary>
/// ゲーム設定ボタン用スクリプト
/// </summary>
public class GameSettingButton : MonoBehaviour
{
    [SerializeField]
    private bool gameTimeSet;// ゲーム時間設定フラグ
    [SerializeField]
    private bool ghostNumSet;// ゴースト数設定フラグ

    [SerializeField]
    private int setNum = 1;// 設定用単位

    private GameSetting gameSetting;

    // Start is called before the first frame update
    void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
    }

    public void OnClick()
    {
        if (gameTimeSet)
        {
            gameSetting.SetGameTime(setNum);
        }
        if (ghostNumSet)
        {
            gameSetting.SetGhostNum(setNum);
        }
    }
}
