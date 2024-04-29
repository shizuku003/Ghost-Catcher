using UnityEngine;

/// <summary>
/// �Q�[���ݒ�{�^���p�X�N���v�g
/// </summary>
public class GameSettingButton : MonoBehaviour
{
    [SerializeField]
    private bool gameTimeSet;// �Q�[�����Ԑݒ�t���O
    [SerializeField]
    private bool ghostNumSet;// �S�[�X�g���ݒ�t���O

    [SerializeField]
    private int setNum = 1;// �ݒ�p�P��

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
