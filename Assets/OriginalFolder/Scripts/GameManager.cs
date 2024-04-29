using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���v���C�V�[���Ǘ��p�X�N���v�g
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapCheck mapCheck;
    private float gameStartTimer = 3f;// �Q�[���J�n�܂ł̎���
    private bool gameStartFlag;// �Q�[���X�^�[�g�p�t���O
    [SerializeField]
    private float timer = 60f;// �����ݒ�Q�[������
    [SerializeField]
    private float chargeTime = 5f;// �Q�[�����ԉ񕜒l
    private bool timeUpFlag = false;// �^�C���A�b�v�p�t���O
    private Text timerText;
    private Text scoreText;
    private Text resultText;

    private int point = 0;// �l���|�C���g

    private ScoreData scoreData;

    [SerializeField]
    private string resultSceneName = "ResultScene";// ���U���g�V�[����

    private GameSetting gameSetting;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        resultText = GameObject.Find("Result").GetComponent<Text>();

        scoreData = GameObject.Find("ScoreData").GetComponent<ScoreData>();

        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        timer = gameSetting.SendGameTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStartTimer > 0f)
        {
            gameStartTimer -= Time.deltaTime;
            timerText.text = "Ready...";
        }
        else
        {
            if (!gameStartFlag)
            {
                mapCheck.PreMapCheck();
                gameStartFlag = true;
            }
            Timer();
        }
    }

    /// <summary>
    /// �^�C�}�[�p���\�b�h
    /// </summary>
    private void Timer()
    {
        if (timer < 0f)
        {
            timerText.text = "Time Up";
            if (!timeUpFlag)
            {
                resultText.text = "Result: " + point.ToString() + "pts";
                scoreData.ScoreReceive(point);
            }
            timeUpFlag = true;
            resultText.enabled = true;
            SceneManager.LoadScene(resultSceneName);
            return;
        }
        timer -= Time.deltaTime;
        timerText.text = "Timer: " + timer.ToString("F2") + "s";
    }

    /// <summary>
    /// �|�C���g�l�����\�b�h
    /// </summary>
    public void GetPoint()
    {
        if (!timeUpFlag)
        {
            point++;
            timer += chargeTime;
            scoreText.text = "Score: " + point.ToString() + "pts";
        }
    }
}
