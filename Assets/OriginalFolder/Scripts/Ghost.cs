using UnityEngine;

/// <summary>
/// �S�[�X�g�p�X�N���v�g
/// </summary>
public class Ghost : MonoBehaviour
{
    private Transform playerPos;// �v���C���[�ʒu
    private Vector3 startPos;// �S�[�X�g�ړ��n�_
    private Vector3 endPos;// �S�[�X�g�ړ��I�_
    [SerializeField]
    private float speed = 0.5f;// �S�[�X�g�ړ����x
    private float time;// �ړ������p�^�C�}�[

    private MapCheck mapCheck;
    private float[] mapInfo = new float[6];// �ȈՃ}�b�v���
    private Vector3[] positions = new Vector3[6];// �ȈՃ}�b�v���iVector3�^�j

    private float vacuumSpeed = 0.5f;// �z�����ړ����x
    private float vacuumTime;// �z�����ړ������p�^�C�}�[
    private bool vacuumFlag = false;// �z���t���O

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Main Camera").transform;
        mapCheck = GameObject.Find("Main Camera").GetComponent<MapCheck>();
        PositionsInitialized();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerPos.position);// �S�[�X�g����Ƀv���C���[�����������悤��

        if (transform.position != endPos)// �S�[�X�g���ړ����̏I�_�ɂ��ǂ蒅���Ă��Ȃ��ꍇ
        {
            if (vacuumFlag)// �S�[�X�g���z�����̏���
            {
                vacuumTime += Time.deltaTime;
                transform.position = Vector3.MoveTowards(startPos, endPos, vacuumTime * vacuumSpeed);
            }
            else
            {
                time += Time.deltaTime;
                transform.position = Vector3.MoveTowards(startPos, endPos, time * speed);
            }
        }
        else// �S�[�X�g���I�_�ɂ��ǂ蒅�����ꍇ�i�e�ϐ��̏������j
        {
            time = 0f;
            vacuumFlag = false;
            vacuumTime = 0f;
            PositionsInitialized();
        }
    }

    /// <summary>
    /// �ړ��p���W���̏������������\�b�h
    /// </summary>
    private void PositionsInitialized()
    {
        mapInfo = mapCheck.MapInfoSend();
        positions[0] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[0]), Random.Range(mapInfo[2] - 1.5f, mapInfo[3] + 1.5f), Random.Range(mapInfo[4] - 1.5f, mapInfo[5] + 1.5f));
        positions[1] = new Vector3(Random.Range(mapInfo[1], mapInfo[1] + 1.5f), Random.Range(mapInfo[2] - 1.5f, mapInfo[3] + 1.5f), Random.Range(mapInfo[4] - 1.5f, mapInfo[5] + 1.5f));
        positions[2] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[1] + 1.5f), Random.Range(mapInfo[2], mapInfo[2] - 1.5f), Random.Range(mapInfo[4] - 1.5f, mapInfo[5] + 1.5f));
        positions[3] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[1] + 1.5f), Random.Range(mapInfo[3], mapInfo[3] + 1.5f), Random.Range(mapInfo[4] - 1.5f, mapInfo[5] + 1.5f));
        positions[4] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[1] + 1.5f), Random.Range(mapInfo[2] - 1.5f, mapInfo[3] + 1.5f), Random.Range(mapInfo[4], mapInfo[4] - 1.5f));
        positions[5] = new Vector3(Random.Range(mapInfo[0] - 1.5f, mapInfo[1] + 1.5f), Random.Range(mapInfo[2] - 1.5f, mapInfo[3] + 1.5f), Random.Range(mapInfo[5], mapInfo[5] + 1.5f));

        CheckPos();
    }

    /// <summary>
    /// �ړ��p���W�̑I��p���\�b�h
    /// </summary>
    void CheckPos()
    {
        int rndS = Random.Range(0, 5);
        int rndE = Random.Range(0, 5);
        while (rndS == rndE)
        {
            rndE = Random.Range(0, 5);
        }

        startPos = positions[rndS];
        transform.position = startPos;
        endPos = positions[rndE];
    }

    /// <summary>
    /// �z�������p���\�b�h
    /// </summary>
    /// <param name="currentPoint">�S�[�X�g�̌��݈ʒu</param>
    /// <param name="weaponPoint">�z����̌��݈ʒu</param>
    public void Vacuumed(Vector3 currentPoint, Vector3 weaponPoint)
    {
        vacuumFlag = true;
        startPos = currentPoint;
        endPos = weaponPoint;
    }

    /// <summary>
    /// �S�[�X�g�j��p���\�b�h
    /// </summary>
    public void DestroyGhost()
    {
        DestroyObject(gameObject);
    }
}