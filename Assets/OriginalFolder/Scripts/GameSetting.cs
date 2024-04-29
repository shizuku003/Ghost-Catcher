using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �Q�[���ݒ�p�X�N���v�g�i�V���O���g���j
/// </summary>
public class GameSetting : MonoBehaviour
{
    public static GameSetting instance;

    [SerializeField]
    private string SelectSceneName = "StageSelectScene";// �Z���N�g�V�[����

    private float gameTime = 30f;// �����ݒ�Q�[������
    private int ghostNum = 5;// �����ݒ�S�[�X�g��

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
    /// �V���O���g���������\�b�h
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
    /// �Q�[�����Ԑݒ�p���\�b�h
    /// </summary>
    /// <param name="setTime">�ݒ�Q�[������</param>
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
    /// �S�[�X�g���ݒ�p���\�b�h
    /// </summary>
    /// <param name="setGhostNum">�ݒ�S�[�X�g��</param>
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
    /// �����ݒ�Q�[�����ԑ��M�p���\�b�h
    /// </summary>
    /// <returns>�����ݒ�Q�[������</returns>
    public float SendGameTime()
    {
        return gameTime;
    }

    /// <summary>
    /// �ݒ�S�[�X�g�����M�p���\�b�h
    /// </summary>
    /// <returns>�ݒ�S�[�X�g��</returns>
    public int SendGhostNum()
    {
        return ghostNum;
    }
}
