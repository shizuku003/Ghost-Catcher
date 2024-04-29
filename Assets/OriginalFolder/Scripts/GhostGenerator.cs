using UnityEngine;

/// <summary>
/// �S�[�X�g�����p�X�N���v�g
/// </summary>
public class GhostGenerator : MonoBehaviour
{
    [SerializeField]
    private int ghostMaxNum = 1;// �Q�[���V�[���Ő�������S�[�X�g���̍ő�l
    [SerializeField]
    private GameObject[] ghost;// ��������S�[�X�g�̃v���t�@�u
    [SerializeField]
    private string ghostTag = "Ghost";// �S�[�X�g�I�u�W�F�N�g�����^�O��
    private bool firstGenerateFlag = false;// �S�[�X�g�̏��������t���O
    private int rnd;// �S�[�X�g�����p����

    private float timer = 0f;// �S�[�X�g�����p�^�C�}�[
    private float interval = 3f;// �S�[�X�g�����܂ł̃C���^�[�o��
    private int ghostNum = 0;// ���݂̃S�[�X�g��

    private GameSetting gameSetting;

    // Start is called before the first frame update
    void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        ghostMaxNum = gameSetting.SendGhostNum();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstGenerateFlag)// �S�[�X�g�̏��������ȍ~
        {
            timer += Time.deltaTime;
            if (timer > interval)// �C���^�[�o�����Ƃ̏���
            {
                ghostNum = GameObject.FindGameObjectsWithTag(ghostTag).Length;
                if (ghostNum < ghostMaxNum)// ���݂̃S�[�X�g�����ݒ肵���ő�l����ቺ�����ꍇ
                {
                    for (int i = 0; i < (ghostMaxNum - ghostNum); i++)
                    {
                        GenerateGhost();
                    }
                }
            }
        }
    }

    /// <summary>
    /// �S�[�X�g�������\�b�h
    /// </summary>
    public void GenerateGhost()
    {
        rnd = Random.Range(0, ghost.Length - 1);
        Instantiate(ghost[rnd]);
    }

    /// <summary>
    /// �S�[�X�g�����������\�b�h
    /// </summary>
    public void InstantiateGhost()
    {
        for (int i = 0; i < ghostMaxNum; i++)
        {
            GenerateGhost();
        }
        firstGenerateFlag = true;
    }
}
